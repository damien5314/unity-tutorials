using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderingAi : MonoBehaviour
{
	public float Speed = 3.0f;
	public float ObstacleRange = 5.0f;

	private bool _alive;

	void Start()
	{
		_alive = true;
	}

	void Update()
	{
		if (_alive)
		{
			// Why does this always use the Z coordinate? Because it's relative space?
			transform.Translate(0, 0, Speed * Time.deltaTime);
		
			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;
			if (Physics.SphereCast(ray, 0.75f, out hit))
			{
				if (hit.distance < ObstacleRange)
				{
					float angle = Random.Range(-110, 110);
					// Rotating around the Y so that causes the model to rotate horizontally
					transform.Rotate(0, angle, 0);
				}
			}
		}
	}

	public void SetAlive(bool alive)
	{
		_alive = alive;
	}
}