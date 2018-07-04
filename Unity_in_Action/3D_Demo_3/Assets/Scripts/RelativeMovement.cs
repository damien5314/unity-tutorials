using System;
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
	private ControllerColliderHit _contact;
	private Animator _animator;

	private void Start()
	{
		_characterController = GetComponent<CharacterController>();
		_verticalSpeed = MinimumFall;
		_animator = GetComponent<Animator>();
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
			// Clamping the magnitude to the configured movement speed to ensure player moves with same velocity
			// diagonally as horizontally
			movement = Vector3.ClampMagnitude(movement, MovementSpeed);

			// We are only using the target rotation to determine which direction the player model should move,
			// so we need to cache its rotation temporarily while we perform the required calculations
			Quaternion temp = _target.rotation;
			// Now make adjustments to the target rotation based on input from the player (movement vector)
			_target.eulerAngles = new Vector3(0, _target.eulerAngles.y, 0);
			// Movement vector was calculated locally, but we are setting the player's global rotation,
			// so convert the vector to global coordinates
			movement = _target.TransformDirection(movement);
			// Now set the camera rotation back to its original value
			_target.rotation = temp;

			// Set the rotation of the player model smoothly with the lerp algorithm, based on the movement
			// vector we just calculated
			Quaternion direction = Quaternion.LookRotation(movement);
			transform.rotation = Quaternion.Lerp(transform.rotation, direction, RotationSpeed * Time.deltaTime);
		}

		_animator.SetFloat("Speed", movement.sqrMagnitude);
		
		// Calculate movement in the Y direction
		bool hitGround = false;
		RaycastHit hit;

		if (_verticalSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
		{
			// Distance from the player's position to the bottom of the CharacterCollider capsule
			// Dividing by 1.9 instead of 2 to make the distance slightly longer than actual in order to account
			// for tiny inaccuracies in raycasting.
			float check = (_characterController.height + _characterController.radius) / 1.9f;
			// If the distance of the hit is less than the checked distance, the player is on the ground
			hitGround = hit.distance <= check;
		}
		
		if (hitGround)
		{
			// If player hit the ground, allow them to jump with the jump button
			if (Input.GetButtonDown("Jump"))
			{
				_verticalSpeed = JumpSpeed;
			}
			// Otherwise, reset their vertical speed to the base falling speed to keep them grounded
			else
			{
				_verticalSpeed = MinimumFall;
				_animator.SetBool("Jumping", false);
			}
		}
		else
		{
			// If the player isn't on the ground, apply gravity
			// TODO: Should we actually be double-applying Time.deltaTime here?
			_verticalSpeed += Gravity * 5 * Time.deltaTime;
			// Apply a maximum falling speed (terminal velocity), probably optional based on physics requirements
			if (_verticalSpeed < TerminalVelocity)
			{
				_verticalSpeed = TerminalVelocity;
			}

			if (_contact != null)
			{
				_animator.SetBool("Jumping", true);
			}

			// When the raycast downward didn't hit anything, but the player is still "grounded" we run
			// into the edge case where the player is standing on the edge of the platform
			if (_characterController.isGrounded)
			{
				// If the player is facing toward the edge, we replace the movement vector with one
				// facing the player's movement so they don't keep moving the wrong direction
				if (Vector3.Dot(movement, _contact.normal) < 0)
				{
					movement = _contact.normal * MovementSpeed;
				}
				// If they're facing away from the edge, we add to the previous horizontal movement vector
				// so the player keeps their momentum away from the edge
				else
				{
					movement += _contact.normal * MovementSpeed;
				}
			}
		}

		// Apply the final vertical speed value to the Y value of the movement vector
		movement.y = _verticalSpeed;

		// Multiply by `deltaTime` to account for framerate differences in the runtime
		movement *= Time.deltaTime;
		_characterController.Move(movement);
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		_contact = hit;
	}
}
