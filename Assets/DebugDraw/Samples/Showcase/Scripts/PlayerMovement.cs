using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public CharacterController controller;
	public Camera cam;
	public float speed = 12;
	public float gravity = -9.81f;
	public float jumpHeight = 3;
	public float mouseSensitivity = 1.6f;

	private Transform tr;
	private Vector3 velocity;
	private Transform camTransform;
	
	private float xRotation;

	private void Start()
	{
		tr = transform;
		camTransform = cam.transform;
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		DoMouseLook();
		DoMovement();
	}

	private void DoMouseLook()
	{
		if (Application.isEditor)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Cursor.lockState = CursorLockMode.None;
				return;
			}
		}
		
		if (Cursor.lockState != CursorLockMode.Locked)
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
			{
				Cursor.lockState = CursorLockMode.Locked;
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

	private void DoMovement()
	{
		Vector3 input = new Vector3(
			Input.GetAxis("Horizontal"),
			0,
			Input.GetAxis("Vertical"));

		if (controller.isGrounded && velocity.y < 0)
		{
			velocity.y = -1f;
		}

		Vector3 move = tr.right * input.x + tr.forward * input.z;
		velocity.y += gravity * Time.deltaTime;
		
		controller.Move(move * (speed * Time.deltaTime));
		controller.Move(velocity * Time.deltaTime);

		if (Input.GetButtonDown("Jump") && controller.isGrounded)
		{
			velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
		}
	}

}