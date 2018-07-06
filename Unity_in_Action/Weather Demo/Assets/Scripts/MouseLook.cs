using UnityEngine;

[AddComponentMenu("Control Script/FPS Input")]
public class MouseLook : MonoBehaviour
{

	public float HorizontalSensitivity = 3.0f;
	public float VerticalSensitivity = 3.0f;
	public float VerticalAngleMin = -90.0f;
	public float VerticalAngleMax = 90.0f;

	private float _rotationX = 0;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update()
	{
		_rotationX -= Input.GetAxis("Mouse Y") * VerticalSensitivity;
		_rotationX = Mathf.Clamp(_rotationX, VerticalAngleMin, VerticalAngleMax);

		float delta = Input.GetAxis("Mouse X") * HorizontalSensitivity;
		float rotationY = transform.localEulerAngles.y + delta;

		transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
	}
}
