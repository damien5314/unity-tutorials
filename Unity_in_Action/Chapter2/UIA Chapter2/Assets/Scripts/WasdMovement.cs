using UnityEngine;

public class WasdMovement : MonoBehaviour
{

	public float MovementSpeed = 6.0f;
	
	void Update()
	{
		if (Input.GetKey(KeyCode.W))
		{
			MoveZ(MovementSpeed);
		}
		if (Input.GetKey(KeyCode.S))
		{
			MoveZ(-MovementSpeed);
		}
		if (Input.GetKey(KeyCode.D))
		{
			MoveX(MovementSpeed);
		}
		if (Input.GetKey(KeyCode.A))
		{
			MoveX(-MovementSpeed);
		}
	}

	private void MoveX(float delta)
	{
		transform.Translate(delta, 0, 0);
	}

	private void MoveZ(float delta)
	{
		transform.Translate(0, 0, delta);
	}
}
