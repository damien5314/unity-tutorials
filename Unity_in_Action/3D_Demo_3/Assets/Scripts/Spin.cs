using UnityEngine;

public class Spin : MonoBehaviour
{
	public float Speed = 3.0f;

	// Update is called once per frame
	void Update()
	{
		transform.Rotate(0, Speed, 0);
	}
}