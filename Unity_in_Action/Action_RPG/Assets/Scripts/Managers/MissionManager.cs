﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour, IGameManager 
{
	
	public ManagerStatus Status { get; private set; }

	public int CurrentLevel { get; private set; }
	public int MaxLevel { get; private set; }

	public void Startup()
	{
		Debug.Log("Mission manager starting...");

		UpdateData(0, 3);
		
		Status = ManagerStatus.Started;
	}

	public void UpdateData(int currentLevel, int maxLevel)
	{
		CurrentLevel = currentLevel;
		MaxLevel = maxLevel;
	}

	public void GoToNext()
	{
		if (CurrentLevel < MaxLevel)
		{
			CurrentLevel++;
			string levelName = "Level" + CurrentLevel;
			Debug.Log("Loading " + levelName);

			SceneManager.LoadScene(levelName);
		}
		else
		{
			Debug.Log("Last level");
			Messenger.Broadcast(GameEvent.GAME_COMPLETE);
		}
	}

	public void ReachObjective()
	{
		// could have logic to handle multiple objectives
		Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
	}

	public void RestartCurrent()
	{
		string levelName = "Level" + CurrentLevel;
		Debug.Log("Restarting " + levelName);
		SceneManager.LoadScene(levelName);
	}
}
