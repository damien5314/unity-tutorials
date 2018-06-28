using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float HorizontalSensitivity = 6.0f;
	public float MovementSpeed = 1.0f;

	private CharacterController _characterController;

	void Start()
	{
		_characterController = GetComponent<CharacterController>();
	}

	void Update()
	{
		UpdateRotation();
		UpdateMovement();
	}

	void UpdateRotation()
	{
		float xDir = Input.GetAxis("Mouse X");
		transform.Rotate(0, xDir * HorizontalSensitivity, 0, Space.Self);
	}

	void UpdateMovement()
	{
		float xMove = Input.GetAxis("Horizontal") * MovementSpeed;
		float zMove = Input.GetAxis("Vertical") * MovementSpeed;

		Vector3 movement = Vector3.ClampMagnitude(new Vector3(xMove, 0, zMove), MovementSpeed);
		movement *= Time.deltaTime;
		movement = transform.TransformDirection(movement);

		_characterController.Move(movement);
	}
}
