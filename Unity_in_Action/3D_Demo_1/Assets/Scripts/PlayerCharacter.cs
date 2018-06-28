using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

	public int Health = 5;
	
	private int _remainingHealth;

	void Start()
	{
		_remainingHealth = Health;
	}

	public void Hurt(int damage)
	{
		_remainingHealth -= damage;
		Debug.Log("Health: " + _remainingHealth);
	}
}
