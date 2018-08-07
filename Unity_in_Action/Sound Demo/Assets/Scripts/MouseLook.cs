using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class MouseLook : MonoBehaviour
{
	public enum RotationAxes
	{
		MouseXY = 0,
		MouseX = 1,
		MouseY = 2
	}

	public RotationAxes Axes = RotationAxes.MouseXY;
	public float HorizontalSensitivity = 3.0f;
	public float VerticalSensitivity = 3.0f;
	public float VerticalAngleMin = -90.0f;
	public float VerticalAngleMax = 90.0f;

	private float _rotationX = 0;

	void Start()
	{
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		if (rigidbody != null)
		{
			rigidbody.freezeRotation = true;
		}
	}

	void Update()
	{
		if (Axes == RotationAxes.MouseX)
		{
			float delta = Input.GetAxis("Mouse X") * HorizontalSensitivity;
			float rotationY = transform.localEulerAngles.y + delta;

			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationY, 0);
		}
		else if (Axes == RotationAxes.MouseY)
		{
			_rotationX -= Input.GetAxis("Mouse Y") * VerticalSensitivity;
			_rotationX = Mathf.Clamp(_rotationX, VerticalAngleMin, VerticalAngleMax);
			float rotationY = transform.localEulerAngles.y;
			transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
		}
		else
		{
			_rotationX -= Input.GetAxis("Mouse Y") * VerticalSensitivity;
			_rotationX = Mathf.Clamp(_rotationX, VerticalAngleMin, VerticalAngleMax);

			float delta = Input.GetAxis("Mouse X") * HorizontalSensitivity;
			float rotationY = transform.localEulerAngles.y + delta;

			transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
		}
	}
}
