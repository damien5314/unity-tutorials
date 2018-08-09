﻿using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
	public ManagerStatus Status { get; private set; }

	public int health { get; private set; }
	public int maxHealth { get; private set; }

	private NetworkService _network;

	public void Startup(NetworkService service)
	{
		Debug.Log("Player manager starting...");

		_network = service;

		// these values could be initialized with saved data
		health = 50;
		maxHealth = 100;

		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		Status = ManagerStatus.Started;
	}

	public void ChangeHealth(int value)
	{
		health += value;
		if (health > maxHealth)
		{
			health = maxHealth;
		}
		else if (health < 0)
		{
			health = 0;
		}

		Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
	}
}