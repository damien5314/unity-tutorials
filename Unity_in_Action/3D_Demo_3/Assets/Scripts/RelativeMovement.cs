using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{

	[SerializeField] private Transform _target; // Reference to the camera
	public float RotationSpeed = 15.0f;
	public float MovementSpeed = 6.0f;
	public float JumpSpeed = 15.0f;
	public float Gravity = -9.8f;
	public float TerminalVelocity = -10.0f;
	public float MinimumFall = -1.5f;

	private CharacterController _characterController;
	private float _verticalSpeed;

	private void Start()
	{
		_characterController = GetComponent<CharacterController>();
		_verticalSpeed = MinimumFall;
	}

	private void Update()
	{
		Vector3 movement = Vector3.zero;

		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		// Calculate movement in the X and Z directions based on player input
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
		
		// Calculate movement in the Y direction based on gravity
		if (_characterController.isGrounded)
		{
			if (Input.GetButtonDown("Jump"))
			{
				_verticalSpeed = JumpSpeed;
			}
			else
			{
				_verticalSpeed = MinimumFall;
			}
		}
		else
		{
			// TODO: Should we actually be double-applying Time.deltaTime here?
			_verticalSpeed += Gravity * 5 * Time.deltaTime;
			if (_verticalSpeed < TerminalVelocity)
			{
				_verticalSpeed = TerminalVelocity;
			}
		}

		movement.y = _verticalSpeed;

		movement *= Time.deltaTime;
		_characterController.Move(movement);
	}
}
