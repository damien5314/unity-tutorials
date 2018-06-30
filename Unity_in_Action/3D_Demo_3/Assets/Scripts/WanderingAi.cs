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

	[SerializeField] private GameObject _fireballPrefab;
	private GameObject _fireball;

	void Start()
	{
		_alive = true;
	}

	void Update()
	{
		if (_alive)
		{
			transform.Translate(0, 0, Speed * Time.deltaTime);
		
			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;
			if (Physics.SphereCast(ray, 0.75f, out hit))
			{
				// Enemy releases a fireball toward the PlayerCharacter
				GameObject hitObject = hit.transform.gameObject;
				if (hitObject.GetComponent<PlayerCharacter>())
				{
					if (_fireball == null)
					{
						_fireball = Instantiate(_fireballPrefab);
						_fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
						_fireball.transform.rotation = transform.rotation;
					}
				}
				// Makes the enemy avoid non-player obstacles
				else if (hit.distance < ObstacleRange)
				{
					float angle = Random.Range(-110, 110);
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