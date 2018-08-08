using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PointClickMovement : MonoBehaviour {

	/// <summary>
	/// Enum representing integer constants returned from <see cref="Input.GetMouseButton"/>.
	/// </summary>
	/// <remarks>
	/// Constants pulled from https://docs.unity3d.com/ScriptReference/Input.GetMouseButton.html
	/// </remarks>
	private enum MouseButton
	{
		LEFT = 0,
		RIGHT = 1,
		MIDDLE = 2
	}
	
	[SerializeField] private Transform target;

	public float moveSpeed = 6.0f;
	public float rotSpeed = 15.0f;
	public float jumpSpeed = 15.0f;
	public float gravity = -9.8f;
	public float terminalVelocity = -20.0f;
	public float minFall = -1.5f;
	public float pushForce = 3.0f;
	public float deceleration = 20.0f;
	public float targetBuffer = 1.5f;

	private float _vertSpeed;
	private ControllerColliderHit _contact;
	private float _currentSpeed = 0f;
	private Vector3 _targetPosition = Vector3.one;

	private CharacterController _charController;
	private Animator _animator;

	// Use this for initialization
	void Start()
	{
		_vertSpeed = minFall;

		_charController = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		// start with zero and add movement components progressively
		Vector3 movement = Vector3.zero;

		// Set the target position when we click with left mouse button
		if (Input.GetMouseButton((int) MouseButton.LEFT) && !EventSystem.current.IsPointerOverGameObject())
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit mouseHit;
			if (Physics.Raycast(ray, out mouseHit))
			{
				GameObject hitObject = mouseHit.transform.gameObject;
				if (hitObject.layer == LayerMask.NameToLayer("Ground"))
				{
					_targetPosition = mouseHit.point;
					_currentSpeed = moveSpeed;
				}
			}
		}

		// Move to target position, if we set one
		if (_targetPosition != Vector3.one)
		{
			// Adjust the Y value to match that of the player
			Vector3 adjustedPosition = new Vector3(_targetPosition.x, transform.position.y, _targetPosition.z);
			
			// Rotate toward the target
			Quaternion targetRotation = Quaternion.LookRotation(adjustedPosition - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);

			// Adjust for current speed of the player
			Vector3 speedAdjustedMovement = _currentSpeed * Vector3.forward;
			
			// Transform direction vector from local coordinates to world coordinates
			movement = transform.TransformDirection(speedAdjustedMovement);

			// If we get within the specified buffer distance from the target
			if (Vector3.Distance(_targetPosition, transform.position) < targetBuffer)
			{
				// Decelerate movement speed by deceleration factor
				_currentSpeed -= deceleration * Time.deltaTime;
				
				// If we decelerated to a stop, reset target position
				if (_currentSpeed <= 0)
				{
					_targetPosition = Vector3.one;
				}
			}
		}

		_animator.SetFloat("Speed", movement.sqrMagnitude);

		// raycast down to address steep slopes and dropoff edge
		bool hitGround = false;
		RaycastHit hit;
		if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
		{
			float check = (_charController.height + _charController.radius) / 1.9f;
			hitGround = hit.distance <= check; // to be sure check slightly beyond bottom of capsule
		}

		// y movement: possibly jump impulse up, always accel down
		// could _charController.isGrounded instead, but then cannot workaround dropoff edge
		if (hitGround)
		{
			_vertSpeed = minFall;
		}
		else
		{
			_vertSpeed += gravity * 5 * Time.deltaTime;
			if (_vertSpeed < terminalVelocity)
			{
				_vertSpeed = terminalVelocity;
			}

			// workaround for standing on dropoff edge
			if (_charController.isGrounded)
			{
				if (Vector3.Dot(movement, _contact.normal) < 0)
				{
					movement = _contact.normal * moveSpeed;
				}
				else
				{
					movement += _contact.normal * moveSpeed;
				}
			}
		}

		movement.y = _vertSpeed;

		movement *= Time.deltaTime;
		_charController.Move(movement);
	}

	// store collision to use in Update
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		_contact = hit;

		Rigidbody body = hit.collider.attachedRigidbody;
		if (body != null && !body.isKinematic)
		{
			body.velocity = hit.moveDirection * pushForce;
		}
	}
}
