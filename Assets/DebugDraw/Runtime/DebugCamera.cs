using UnityEngine;

namespace DebugDrawUtils
{

	/// <summary>
	/// A simple free floating debug camera.
	/// Don't create directly, instead use <see cref="DebugDraw.ToggleDebugCamera(bool)"/>
	/// </summary>
    public class DebugCamera : MonoBehaviour
    {

		/// <summary>
		/// The attached camera.
		/// </summary>
		public Camera cam { get; protected set; }
		/// <summary>
		/// The speed of the mouse while looking around.
		/// </summary>
		public float mouseSensitivity = 1.6f;
		/// <summary>
		/// The acceleration.
		/// </summary>
		public float acceleration = 100;
		/// <summary>
		/// The max speed the camera can move.
		/// </summary>
		public float maxSpeed = 18;
		/// <summary>
		/// A speed multiplier when holding the shift key.
		/// </summary>
		public float fastMultiplier = 2;
		/// <summary>
		/// A speed multiplier when holding the alt key.
		/// </summary>
		public float slowMultiplier = 0.5f;
		/// <summary>
		/// A speed multiplier that can be adjust with ctrl + mouse wheel.
		/// 0 = x 1, -1 = x minSpeedMultiple, 1 = x maxSpeedMultiple.
		/// </summary>
		public float currentSpeedPercent = 0;
		/// <summary>
		/// The minimum value that currentSpeedMultiplier can be.
		/// </summary>
		public float minSpeedMultiplier = 0.25f;
		/// <summary>
		/// The maximum value that currentSpeedMultiplier can be.
		/// </summary>
		public float maxSpeedMultiplier = 12;
		/// <summary>
		/// Affects how fast the camera will slow down when not moving.
		/// </summary>
		public float drag = 15f;

		/// <summary>
		/// If larger than zero, draws cross hairs in the centre of the screen, specified in hundredths of world coordinates.
		/// The cross hair is faked by drawing two lines right in front of the camera's near clipping plane.
		/// </summary>
		public float crossHairSize;
		/// <summary>
		/// The color of the cross hairs.
		/// </summary>
		public Color crossHairColor = new Color(0.75f, 0.75f, 0.75f, 1);

		protected Transform camTr;
		protected Transform tr;

		protected float speed;
		protected Vector3 direction;
    	protected Vector2 rotation;
		protected float baseFOV;

		/// <summary>
		/// True if an object is being tracked.
		/// </summary>
		public bool isTrackingObj { get; protected set; }
		/// <summary>
		/// True if the tracked object is also locked into the centre of the view.
		/// </summary>
		public bool isLookingAtObj;
		/// <summary>
		/// The object the debug camera is tracking.
		/// </summary>
		public Transform trackingObj { get; protected set; }
		protected Vector3 trackingObjPosition;

		/* ------------------------------------------------------------------------------------- */
		/* -- Init -- */
    
    	protected virtual void Awake()
    	{
    		tr = transform;
			GameObject camObj = new GameObject("");
			camObj.transform.SetParent(tr, false);
    		cam = camObj.AddComponent<Camera>();
			camTr = cam.transform;
		}
    
    	protected virtual void OnDestroy()
    	{
    		DebugDraw.ToggleDebugCamera(false);
    	}
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Private -- */
    
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
			
			if (crossHairSize > 0)
			{
				Vector3 p = tr.position + camTr.forward * (cam.nearClipPlane + 0.0001f);
				Vector3 r = camTr.right;
				Vector3 u = camTr.up;
				float s = crossHairSize * 0.01f;

				DebugDraw.Line(
					new Vector3(
						p.x - r.x * s,
						p.y - r.y * s,
						p.z - r.z * s),
					new Vector3(
						p.x + r.x * s,
						p.y + r.y * s,
						p.z + r.z * s), crossHairColor);
				DebugDraw.Line(
					new Vector3(
						p.x - u.x * s,
						p.y - u.y * s,
						p.z - u.z * s),
					new Vector3(
						p.x + u.x * s,
						p.y + u.y * s,
						p.z + u.z * s), crossHairColor);
			}

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

					int p = Mathf.FloorToInt((currentSpeedMultiplier * this.maxSpeed) / this.maxSpeed * 100);
					Log.Show(0xffffff - 1, 1, $"Debug Camera Speed: {p}%");
				}
			}
			else if (scroll != 0)
			{
				cam.fieldOfView = Mathf.Clamp(cam.fieldOfView + scroll * -3, 2, 170);
				Log.Show(0xffffff - 1, 1, $"Debug Camera FOV: {(int) cam.fieldOfView}");
			}
			
			float multiplier = currentSpeedMultiplier * (fast ? fastMultiplier : slow ? slowMultiplier : 1);
			float acceleration = this.acceleration * multiplier;
			float maxSpeed = this.maxSpeed * multiplier;
			float drag = this.drag * multiplier;

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

		internal virtual void UpdateCamera(Camera from, bool copyTransform, bool copyProperties)
		{
			if (!from)
				return;

			if (copyTransform)
			{
				Transform newTr = from.transform;
				tr.position = newTr.position;
				tr.localScale = newTr.localScale;

				Quaternion rot = newTr.rotation;
				rotation.x = Mathf.Atan2(2 * rot.x * rot.w - 2 * rot.y * rot.z, 1 - 2 * rot.x * rot.x - 2 * rot.z * rot.z) * Mathf.Rad2Deg;
				rotation.y = Mathf.Atan2(2 * rot.y * rot.w - 2 * rot.x * rot.z, 1 - 2 * rot.y * rot.y - 2 * rot.z * rot.z) * Mathf.Rad2Deg;
				camTr.localRotation = Quaternion.Euler(rotation.x, 0, 0);
				tr.rotation = Quaternion.Euler(0, rotation.y, 0);
			}

			if (copyProperties)
			{
				cam.CopyFrom(from);
			}
		}
    
    	internal virtual void Toggle(bool on)
		{
			speed = 0;
			baseFOV = cam ? cam.fieldOfView : 60;

			if (on)
			{
				LockCursor(true);
			}
		}
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		/// <summary>
		/// The debug camera will attempt to keep its relative position to this object every frame.
		/// </summary>
		/// <param name="obj">The object to track. Set to null to stop tracking.</param>
		/// <param name="lookAt">If true the object will also stay centred in the view.</param>
		public void TrackObject(Transform obj, bool lookAt = false)
		{
			trackingObj = obj;
			isTrackingObj = obj != null;
			isLookingAtObj = lookAt;

			if (isTrackingObj)
			{
				trackingObjPosition = obj.position;
			}
		}

		/// <inheritdoc cref="TrackObject(UnityEngine.Transform, bool)"/>
		public void TrackObject(GameObject obj, bool lookAt = false)
		{
			TrackObject(obj ? obj.transform : null, lookAt);
		}

		/// <inheritdoc cref="TrackObject(UnityEngine.Transform, bool)"/>
		public void TrackObject(MonoBehaviour obj, bool lookAt = false)
		{
			TrackObject(obj ? obj.transform : null, lookAt);
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