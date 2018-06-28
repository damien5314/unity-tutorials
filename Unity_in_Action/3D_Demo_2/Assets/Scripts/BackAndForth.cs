using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{

	public float Speed = 3.0f;
	public float MinZ = -16.0f;
	public float MaxZ = 16.0f;

	private int _direction = 1;

	void Update()
	{
		transform.Translate(0, 0, _direction * Speed * Time.deltaTime);

		bool bounced = false;

		if (transform.position.z > MaxZ || transform.position.z < MinZ)
		{
			_direction = -_direction;
			bounced = true;
		}

		if (bounced)
		{
			transform.Translate(0, 0, _direction * Speed * Time.deltaTime);
		}
	}
}
