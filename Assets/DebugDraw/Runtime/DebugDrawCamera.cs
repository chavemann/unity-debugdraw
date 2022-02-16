#if !DEBUG_DRAW_OFF
#define DEBUG_DRAW
#endif

using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace DebugDrawUtils
{

	/// <summary>
	/// A simple free floating debug camera.
	/// Don't create directly, instead use <see cref="DebugDrawCamera.Toggle(bool)"/>
	/// </summary>
    public class DebugDrawCamera : MonoBehaviour
    {

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RuntimeInit()
		{
			hasCrossHairMaterial = false;
		}
		
		/// <summary>
		/// True if the debug camera is active.
		/// </summary>
		public static bool active { get; private set; }
		/// <summary>
		/// The current DebugDrawCamera, may be null.
		/// </summary>
		public static DebugDrawCamera instance { get; protected set; }
		/// <summary>
		/// The method responsible for creating the debug camera.
		/// Can be used to extend DebugCamera with custom functionality.
		/// Leave null for the default.
		/// If set this method should create a DebugCamera (or subclass of) on the given game object and return the result.
		/// </summary>
		public static Func<GameObject, DebugDrawCamera> factory;

		/// <summary>
		/// The attached camera.
		/// </summary>
		public static Camera cam { get; protected set; }
		/// <summary>
		/// The speed of the mouse while looking around.
		/// </summary>
		public static float mouseSensitivity = 1.6f;
		/// <summary>
		/// The acceleration.
		/// </summary>
		public static float acceleration = 100;
		/// <summary>
		/// The max speed the camera can move.
		/// </summary>
		public static float maxSpeed = 18;
		/// <summary>
		/// A speed multiplier when holding the shift key.
		/// </summary>
		public static float fastMultiplier = 2;
		/// <summary>
		/// A speed multiplier when holding the alt key.
		/// </summary>
		public static float slowMultiplier = 0.5f;
		/// <summary>
		/// A speed multiplier that can be adjust with ctrl + mouse wheel.
		/// 0 = x 1, -1 = x minSpeedMultiple, 1 = x maxSpeedMultiple.
		/// </summary>
		public static float currentSpeedPercent = 0;
		/// <summary>
		/// The minimum value that currentSpeedMultiplier can be.
		/// </summary>
		public static float minSpeedMultiplier = 0.25f;
		/// <summary>
		/// The maximum value that currentSpeedMultiplier can be.
		/// </summary>
		public static float maxSpeedMultiplier = 12;
		/// <summary>
		/// Affects how fast the camera will slow down when not moving.
		/// </summary>
		public static float drag = 15f;

		/// <summary>
		/// If larger than zero, draws cross hairs in the centre of the screen.
		/// </summary>
		public static float crossHairSize;
		/// <summary>
		/// The color of the cross hairs.
		/// </summary>
		public static Color crossHairColor = new Color(0.75f, 0.75f, 0.75f, 1);

		/// <summary>
		/// True if an object is being tracked.
		/// </summary>
		public static bool isTrackingObj { get; protected set; }
		/// <summary>
		/// True if the tracked object is also locked into the centre of the view.
		/// </summary>
		public static bool isLookingAtObj;
		/// <summary>
		/// The object the debug camera is tracking.
		/// </summary>
		public static Transform trackingObj { get; protected set; }
		protected static Vector3 trackingObjPosition;

		public static Action<bool> onToggle;

		protected static Transform camTr;
		protected static Transform tr;
		
		protected static Camera lastCamera;
		protected static float baseFOV;

		protected static float speed;
		protected static Vector3 direction;
    	protected static Vector2 rotation;

		protected static bool hasCrossHairMaterial;
		protected static Material crossHairMaterial;
		protected static Mesh crossHairMesh;

		/* ------------------------------------------------------------------------------------- */
		/* -- Init -- */
		
		/// <summary>
		/// <para>Turns the debug camera on or off.
		/// When active, creates a new camera that can freely fly around with the WASD keys and mouse.</para>
		/// <list type="bullet">
		/// 	<item><b>W/A/S/D:</b> Move</item>
		/// 	<item><b>Q/E/Space:</b> rise/fall</item>
		/// 	<item><b>Mouse:</b> Look</item>
		/// 	<item><b>Shift:</b> Move fast</item>
		/// 	<item><b>Ctrl:</b> Move slow</item>
		/// 	<item><b>Alt </b>+ wheel: Adjust speed</item>
		/// 	<item><b>Wheel:</b> Adjust zoom</item>
		/// 	<item><b>Home:</b> Reset speed and zoom</item>
		/// </list>
		/// </summary>
		/// <param name="on">On or off.</param>
		public static void Toggle(bool on)
		{
			if (!Application.isPlaying)
				return;
			
			if (active == on)
				return;
			
			active = on;

			if (active)
			{
				lastCamera = Camera.main;

				if (lastCamera != null)
				{
					lastCamera.gameObject.SetActive(false);
				}

				if (!instance)
				{
					GameObject obj = new GameObject("__DebugDrawCam__") { hideFlags = HideFlags.NotEditable };

					if (factory != null)
					{
						instance = factory(obj);

						if (!instance)
						{
							Console.WriteLine("DebugCamera component returned from debugCameraFactory should not be null.");
							Destroy(obj);
							active = false;
							return;
						}

						if (instance.gameObject != obj)
						{
							Console.WriteLine("DebugCamera was not added to the provided game object.");
						}
					}
					else
					{
						instance = obj.AddComponent<DebugDrawCamera>();
					}
					
					instance.hideFlags = HideFlags.NotEditable;
					
					if (lastCamera != null)
					{
						UpdateCamera(lastCamera, true, true);
					}
				}
				
				if (lastCamera != null)
				{
					lastCamera.gameObject.SetActive(false);
				}
				
				instance.gameObject.SetActive(true);
			}
			else
			{
				if (instance)
				{
					instance.gameObject.SetActive(false);
				}

				if (lastCamera)
				{
					lastCamera.gameObject.SetActive(true);
				}
			}

			if (instance)
			{
				instance.Init();
			}
			
			DebugDraw.InitCamera(active ? cam : lastCamera);
		}

		/// <inheritdoc cref="Toggle(bool)"/>
		public static void Toggle()
		{
			Toggle(!active);
		}

		/// <summary>
		/// Copies the transform and optionally properties of the given camera.
		/// </summary>
		/// <param name="from">If null uses the last active camera when the debug camera was activated.</param>
		/// <param name="copyTransform">Sets the debug camera's position and rotation to match the given camera.</param>
		/// <param name="copyProperties">Sets the debug camera's properties to match the given camera.</param>
		public static void UpdateCamera(Camera from = null, bool copyTransform = true, bool copyProperties = false)
		{
			instance.CopyFrom(from, copyTransform, copyProperties);
		}
		
		/// <summary>
		/// Called whenever the the camera is toggled.
		/// </summary>
		protected virtual void Init()
		{
			if (!active)
				return;
			
			speed = 0;
			baseFOV = cam ? cam.fieldOfView : 60;
			LockCursor(true);
		}
		
		protected virtual void Awake()
		{
			tr = transform;
			GameObject camObj = new GameObject("Cam");
			camObj.transform.SetParent(tr, false);
			cam = camObj.AddComponent<Camera>();
			camTr = cam.transform;
		}
		
		protected virtual void OnDestroy()
		{
			Toggle(false);
		}

    
		/* ------------------------------------------------------------------------------------- */
		/* -- Private -- */

		protected virtual void OnRenderObject()
		{
			if (crossHairSize > 0)
			{
				if (!hasCrossHairMaterial)
				{
					crossHairMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));
					crossHairMaterial.hideFlags = HideFlags.HideAndDontSave;
					// Turn on alpha blending
					crossHairMaterial.SetInt(DebugDrawMesh.SrcBlend, (int) BlendMode.SrcAlpha);
					crossHairMaterial.SetInt(DebugDrawMesh.DstBlend, (int) BlendMode.OneMinusSrcAlpha);
					// Turn backface culling off
					crossHairMaterial.SetInt(DebugDrawMesh.Cull, (int) CullMode.Off);
					// Turn off depth writes
					crossHairMaterial.SetInt(DebugDrawMesh.ZWrite, 0);
					crossHairMaterial.SetInt(DebugDrawMesh.ZTest, (int) CompareFunction.Always);
					
					crossHairMesh = new Mesh();
					crossHairMesh.SetVertices(new Vector3[]
					{
						Vector3.left, Vector3.right,
						Vector3.up, Vector3.down, 
					});
					crossHairMesh.SetIndices(new int[] { 0, 1, 2, 3 }, MeshTopology.Lines, 0);

					hasCrossHairMaterial = true;
				}
				
				float s = crossHairSize * 0.01f;
				
				crossHairMaterial.SetColor(DebugDrawMesh.Color, crossHairColor);
				crossHairMaterial.SetPass(0);
				
				Graphics.DrawMeshNow(
					crossHairMesh,
					Matrix4x4.TRS(
						tr.position + camTr.forward * (cam.nearClipPlane + 0.001f),
						camTr.rotation,
						new Vector3(s, s, s)));
			}
		}

		protected virtual void LateUpdate()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				LockCursor(false);
			}
			else if (Cursor.lockState != CursorLockMode.Locked)
			{
				if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
				{
					LockCursor(true);
				}
			}

			DoMouseLook();
			DoMovement();
			
			if (isTrackingObj)
			{
				if (trackingObj)
				{
					Vector3 newPosition = trackingObj.position;
					Vector3 position = tr.position;
					
					if (isLookingAtObj)
					{
						float dist = new Vector3(
							position.x - trackingObjPosition.x,
							position.y - trackingObjPosition.y,
							position.z - trackingObjPosition.z).magnitude;
						position = trackingObjPosition - camTr.forward * dist;
					}

					position += new Vector3(
						newPosition.x - trackingObjPosition.x,
						newPosition.y - trackingObjPosition.y,
						newPosition.z - trackingObjPosition.z);
					tr.position = position;
					
					trackingObjPosition = newPosition;
				}
				else
				{
					isTrackingObj = false;
					trackingObj = null;
				}
			}
		}

		protected virtual void DoMouseLook()
		{
			Vector2 mouse = Cursor.lockState == CursorLockMode.Locked
				? new Vector2(
					Input.GetAxis("Mouse X"),
					Input.GetAxis("Mouse Y")) * mouseSensitivity
				: Vector2.zero;
			
			rotation.x = Mathf.Clamp(rotation.x - mouse.y, -90, 90);
			rotation.y = Mathf.Repeat(rotation.y + mouse.x, 360);
			
			camTr.localRotation = Quaternion.Euler(rotation.x, 0, 0);
			tr.rotation = Quaternion.Euler(0, rotation.y, 0);
		}

		protected virtual void DoMovement()
		{
			Vector3 forward = camTr.forward;
			Vector3 right = camTr.right;
			Vector3 input = Cursor.lockState == CursorLockMode.Locked
				? new Vector3(
					Input.GetAxisRaw("Horizontal"),
					Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Space)
						? 1
						: Input.GetKey(KeyCode.E) ? -1 : 0,
					Input.GetAxisRaw("Vertical"))
				: Vector3.zero;
			
			Vector3 move = forward * input.z + right * input.x + Vector3.up * input.y;
			bool isMoving = move != Vector3.zero;
			
			float scroll = Input.mouseScrollDelta.y;
			bool alt = Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
			bool fast = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift);
			bool slow = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
			float currentSpeedMultiplier = CalculateSpeedMultiplier();

			if (Input.GetKeyDown(KeyCode.Home))
			{
				cam.fieldOfView = baseFOV;
				currentSpeedPercent = 0;
			}
			else if (alt)
			{
				if(scroll != 0)
				{
					const float steps = 0.05f;
					currentSpeedPercent = Mathf.Clamp(currentSpeedPercent + scroll * steps, -1, 1);
					currentSpeedPercent = Mathf.Round(currentSpeedPercent / steps) * steps;
					currentSpeedMultiplier = CalculateSpeedMultiplier();

					int p = Mathf.FloorToInt((currentSpeedMultiplier * DebugDrawCamera.maxSpeed) / DebugDrawCamera.maxSpeed * 100);
					Log.Show(0xffffff - 1, 1, $"Debug Camera Speed: {p}%");
				}
			}
			else if (scroll != 0)
			{
				cam.fieldOfView = Mathf.Clamp(cam.fieldOfView + scroll * -3, 2, 170);
				Log.Show(0xffffff - 1, 1, $"Debug Camera FOV: {(int) cam.fieldOfView}");
			}
			
			float multiplier = currentSpeedMultiplier * (fast ? fastMultiplier : slow ? slowMultiplier : 1);
			float acceleration = DebugDrawCamera.acceleration * multiplier;
			float maxSpeed = DebugDrawCamera.maxSpeed * multiplier;
			float drag = DebugDrawCamera.drag * multiplier;

			if (isMoving)
			{
				move.Normalize();
				speed += acceleration * Time.deltaTime;
				direction = move;
			}
			else
			{
				float frictionF = 1 / (1 + (drag + speed * 0.01f) * Time.deltaTime);
				speed *= frictionF;
			}

			speed = Mathf.Clamp(speed, 0, maxSpeed);

			if (speed < 0.002f)
			{
				speed = 0;
			}

			Vector3 velocity = direction * speed;
			tr.position += velocity * Time.deltaTime;
		}

		protected virtual float CalculateSpeedMultiplier()
		{
			return currentSpeedPercent < 0
				? Mathf.Lerp(1, minSpeedMultiplier, Mathf.Abs(currentSpeedPercent))
				: Mathf.Lerp(1, maxSpeedMultiplier, currentSpeedPercent);
		}

		protected virtual void CopyFrom(Camera from, bool copyTransform, bool copyProperties)
		{
			if (!from)
			{
				from = lastCamera;
				
				if (!from)
					return;
			}

			if (copyTransform)
			{
				Transform newTr = from.transform;
				tr.position = newTr.position;
				tr.localScale = newTr.localScale;

				CalculateRotation(newTr.rotation);
			}
			
			if (copyProperties)
			{
				cam.CopyFrom(from);
			}
		}

		protected static void CalculateRotation(Quaternion rot)
		{
			rotation.x = Mathf.Atan2(2 * rot.x * rot.w - 2 * rot.y * rot.z, 1 - 2 * rot.x * rot.x - 2 * rot.z * rot.z) * Mathf.Rad2Deg;
			rotation.y = Mathf.Atan2(2 * rot.y * rot.w - 2 * rot.x * rot.z, 1 - 2 * rot.y * rot.y - 2 * rot.z * rot.z) * Mathf.Rad2Deg;
			camTr.localRotation = Quaternion.Euler(rotation.x, 0, 0);
			tr.rotation = Quaternion.Euler(0, rotation.y, 0);
		}
    
		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		/// <summary>
		/// The debug camera will attempt to keep its relative position to this object every frame.
		/// </summary>
		/// <param name="obj">The object to track.</param>
		/// <param name="lookAt">If true the object will also stay centred in the view.</param>
		public static void TrackObject(GameObjectOrTransform obj, bool lookAt = false)
		{
			bool prevTrackingObj = isTrackingObj;
			
			trackingObj = obj;
			isTrackingObj = trackingObj != null;
			isLookingAtObj = lookAt;

			if (isTrackingObj)
			{
				trackingObjPosition = trackingObj.position;

				if (lookAt && !prevTrackingObj)
				{
					CalculateRotation(
						Quaternion.LookRotation((trackingObjPosition - tr.position).normalized, Vector3.up));
				}
			}
		}

		public static void LockCursor(bool locked)
		{
			if (locked)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			else
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}
		
	}

}