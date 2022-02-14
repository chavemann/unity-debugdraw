using UnityEngine;

namespace DebugDrawSamples.Showcase.Scripts
{

	[RequireComponent(typeof(CharacterController))]
	public class PlayerMovement : MonoBehaviour
	{

		public Camera cam;
		public float groundAcceleration = 65;
		public float sprintAcceleration = 100;
		public float airAcceleration = 15;
		public float maxSpeed = 8;
		public float maxSprintSpeed = 16;
		public float airFriction = 0.5f;
		public float groundFriction = 25;
		public float gravity = -20;
		public float jumpHeight = 3;
		public float mouseSensitivity = 1.6f;

		private Transform tr;
		private Transform camTransform;
		private CharacterController controller;
	
		private float xRotation;
		private float stepOffset;
	
		private Vector3 velocity;
		private bool grounded;
		private Vector3 groundNormal = Vector3.up;
		private Vector3 feetPos;

		private void Start()
		{
			tr = transform;
			camTransform = cam.transform;
			LockCursor(true);

			controller = GetComponent<CharacterController>();
			stepOffset = controller.stepOffset;
		}

		private void Update()
		{
			DoMouseLook();
			DoMovement();
		}

		private void DoMouseLook()
		{
			if (DebugDraw.usingDebugCamera)
				return;
			
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				#if UNITY_EDITOR
				if (Application.isEditor && Cursor.lockState == CursorLockMode.None)
				{
					UnityEditor.EditorApplication.isPlaying = false;
				}
				#endif
				
				LockCursor(false);
				return;
			}
		
			if (Cursor.lockState != CursorLockMode.Locked)
			{
				if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
				{
					LockCursor(true);
				}
				else
				{
					return;
				}
			}
		
			Vector2 mouse = new Vector2(
				Input.GetAxis("Mouse X"),
				Input.GetAxis("Mouse Y")) * mouseSensitivity;

			xRotation = Mathf.Clamp(xRotation - mouse.y, -90, 90);

			camTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
			tr.Rotate(Vector3.up * mouse.x);
		}
		
		private void LockCursor(bool locked)
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

		private void UpdateFeetPos()
		{
			feetPos = tr.localPosition + new Vector3(0, -controller.height * 0.5f - controller.skinWidth, 0);
		}

		private void DoMovement()
		{
			bool sprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift);
			float acceleration = sprinting ? sprintAcceleration : groundAcceleration;
			float maxSpeed = sprinting ? maxSprintSpeed : this.maxSpeed;
			
			UpdateFeetPos();

			bool prevGrounded = grounded;
			Vector3 prevGroundNormal = groundNormal;

			float r = controller.radius;
			if (Physics.SphereCast(feetPos + new Vector3(0, stepOffset + r, 0), r, Vector3.down, out RaycastHit hit, stepOffset * 2))
			{
				if (grounded || hit.distance <= stepOffset)
				{
					grounded = true;
					groundNormal = hit.normal;

					if (!controller.isGrounded && hit.distance > stepOffset)
					{
						Vector3 p = tr.position;
						p.y -= hit.distance - stepOffset;
						controller.enabled = false;
						tr.position = p;
						controller.enabled = true;
						UpdateFeetPos();
					}
				}
				else
				{
					grounded = false;
				}
			}
			else
			{
				grounded = false;
			}
			
			if (!grounded)
			{
				groundNormal = Vector3.up;
			}

			controller.stepOffset = grounded ? stepOffset : 0;

			if (grounded)
			{
				if (prevGrounded && prevGroundNormal != groundNormal)
				{
					float prevSpeed = velocity.magnitude;
					velocity = velocity.normalized * prevSpeed;
					velocity = Vector3.ProjectOnPlane(velocity, groundNormal);
				}
				else
				{
					velocity.y = 0;
				}
			}

			Vector3 forward = tr.forward;
			Vector3 right = tr.right;
			Vector3 input = !DebugDraw.usingDebugCamera ? new Vector3(
				Input.GetAxisRaw("Horizontal"),
				0,
				Input.GetAxisRaw("Vertical")) : Vector3.zero;
			
			if (input.magnitude  > 1)
			{
				input.Normalize();
			}
			
			Vector3 moveDir = right * input.x + forward * input.z;
			bool isWalking = input != Vector3.zero;

			moveDir = Vector3.ProjectOnPlane(moveDir, groundNormal);
			moveDir.Normalize();
			float accFactor = 1 - Vector3.Dot(moveDir, velocity.normalized);

			float speed = new Vector2(velocity.x, velocity.z).magnitude;

			if (grounded && !isWalking)
			{
				velocity.x *= CalcFriction(groundFriction);
				velocity.z *= CalcFriction(groundFriction);
			}
			else if (speed > maxSpeed || grounded)
			{
				if (speed > maxSpeed)
				{
					accFactor = speed / maxSpeed;
				}
				
				velocity.x *= CalcFriction(groundFriction * accFactor);
				velocity.z *= CalcFriction(groundFriction * accFactor);
			}
			
			if (isWalking)
			{
				velocity += moveDir * ((grounded ? acceleration : airAcceleration) * Time.deltaTime);
			}
			
			if (!grounded)
			{
				velocity.y *= CalcFriction(airFriction);
			}
			
			velocity.y += gravity * Time.deltaTime;

			if (Input.GetButtonDown("Jump") && grounded && !DebugDraw.usingDebugCamera)
			{
				velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
				grounded = false;
				groundNormal = Vector3.up;
			}

			controller.Move(velocity * Time.deltaTime);
		}

		private float CalcFriction(float friction)
		{
			return 1 / (1 + friction * Time.deltaTime);
		}

		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			// Ignore ground
			if (
				CheckGroundNormal(hit.normal) ||
				hit.point.y <= feetPos.y + stepOffset)
			{
				groundNormal = hit.normal;
				return;
			}

			velocity = Vector3.ProjectOnPlane(velocity, new Vector3(hit.normal.x, 0, hit.normal.z).normalized);
		}

		private bool CheckGroundNormal(Vector3 normal)
		{
			return Mathf.Acos(Vector3.Dot(normal, Vector3.up)) * Mathf.Rad2Deg <= controller.slopeLimit;
		}

	}

}