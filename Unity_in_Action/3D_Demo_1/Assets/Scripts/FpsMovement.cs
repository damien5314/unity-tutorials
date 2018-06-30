using UnityEngine;

public class FpsMovement : MonoBehaviour
{

	public const float BaseSpeed = 6.0f;
	
	public float MovementSpeed = 6.0f;
	public float Gravity = -9.8f;

	private CharacterController _characterController;

	private void Awake()
	{
		Messenger<float>.AddListener(GameEvent.SpeedChanged, OnSpeedChanged);
	}

	private void OnDestroy()
	{
		Messenger<float>.RemoveListener(GameEvent.SpeedChanged, OnSpeedChanged);
	}

	void Start()
	{
		_characterController = GetComponent<CharacterController>();
		UpdateSpeed(SettingsManager.Speed);
	}

	void Update()
	{
		float deltaX = Input.GetAxis("Horizontal") * MovementSpeed;
		float deltaZ = Input.GetAxis("Vertical") * MovementSpeed;

		// Clamp the magnitude so player doesn't move faster when walking diagonally
		Vector3 movement = new Vector3(deltaX, 0, deltaZ);
		movement = Vector3.ClampMagnitude(movement, MovementSpeed);
		movement.y = Gravity;

		// Adjust for the framerate of the machine
		movement *= Time.deltaTime;

		movement = transform.TransformDirection(movement);

		// Perform the translation
		_characterController.Move(movement);
	}

	private void OnSpeedChanged(float speed)
	{
		UpdateSpeed(speed);
	}

	private void UpdateSpeed(float speedSetting)
	{
		MovementSpeed = BaseSpeed * speedSetting;
	}
}
