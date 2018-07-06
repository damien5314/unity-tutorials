using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

	public float Sensitivity = 6.0f;

	private CharacterController _character;

	private void Start()
	{
		
		_character = GetComponent<CharacterController>();
	}

	private void Update()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		Vector3 movement = Vector3.zero;

		if (horizontal != 0)
		{
			movement.x = horizontal * Sensitivity;
		}

		if (vertical != 0)
		{
			movement.z = vertical * Sensitivity;
		}

		movement = Vector3.ClampMagnitude(movement, Sensitivity);
		movement = transform.TransformDirection(movement);
		movement *= Time.deltaTime;

		_character.Move(movement);
	}
}
