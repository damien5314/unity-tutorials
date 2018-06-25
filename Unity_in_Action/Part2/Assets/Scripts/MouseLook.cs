using UnityEngine;

public class MouseLook : MonoBehaviour
{
	
	public float VerticalSensitivity = 6.0f;
	public float MinRotation = -90.0f;
	public float MaxRotation = 90.0f;

	private float _rotation = 0;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update()
	{
		_rotation -= Input.GetAxis("Mouse Y") * VerticalSensitivity;
		// Prevent camera from flipping upside down
		_rotation = Mathf.Clamp(_rotation, MinRotation, MaxRotation);
		transform.localEulerAngles = new Vector3(_rotation, transform.localEulerAngles.y, 0);
	}
}
