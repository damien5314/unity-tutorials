﻿using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
	public ManagerStatus Status { get; private set; }

	public int health { get; private set; }
	public int maxHealth { get; private set; }

	public void Startup()
	{
		Debug.Log("Player manager starting...");

		UpdateData(50, 100);

		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		Status = ManagerStatus.Started;
	}

	public void UpdateData(int health, int maxHealth)
	{
		this.health = health;
		this.maxHealth = maxHealth;
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

		if (health == 0)
		{
			Messenger.Broadcast(GameEvent.LEVEL_FAILED);
		}

		Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
	}

	public void Respawn()
	{
		UpdateData(50, 100);
	}
}
