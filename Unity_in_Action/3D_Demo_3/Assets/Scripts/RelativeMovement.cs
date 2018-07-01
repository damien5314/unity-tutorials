using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{

	[SerializeField] private Transform _target; // Reference to the camera
	public float RotationSpeed = 15.0f;
	public float MovementSpeed = 6.0f;

	private CharacterController _characterController;

	private void Start()
	{
		_characterController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		Vector3 movement = Vector3.zero;

		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		if (horizontalInput != 0 || verticalInput != 0)
		{
			movement.x = horizontalInput * MovementSpeed;
			movement.z = verticalInput * MovementSpeed;
			movement = Vector3.ClampMagnitude(movement, MovementSpeed);

			Quaternion temp = _target.rotation;
			_target.eulerAngles = new Vector3(0, _target.eulerAngles.y, 0);
			movement = _target.TransformDirection(movement);
			_target.rotation = temp;

			Quaternion direction = Quaternion.LookRotation(movement);
			transform.rotation = Quaternion.Lerp(transform.rotation, direction, RotationSpeed * Time.deltaTime);
		}

		movement *= Time.deltaTime;
		_characterController.Move(movement);
	}
}
