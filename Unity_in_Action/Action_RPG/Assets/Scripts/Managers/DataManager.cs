﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour, IGameManager 
{
	public ManagerStatus Status { get; private set; }

	private string _filename;

	public void Startup()
	{
		Debug.Log("Data manager starting");

		_filename = Path.Combine(Application.persistentDataPath, "game.dat");

		Status = ManagerStatus.Started;
	}

	public void SaveGameState()
	{
		Dictionary<string, object> gameState = new Dictionary<string, object>();
		gameState.Add("inventory", Managers.Inventory.GetData());
		gameState.Add("health", Managers.Player.health);
		gameState.Add("maxHealth", Managers.Player.maxHealth);
		gameState.Add("currentLevel", Managers.Mission.CurrentLevel);
		gameState.Add("maxLevel", Managers.Mission.MaxLevel);

		FileStream stream = File.Create(_filename);
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(stream, gameState);
		stream.Close();
	}

	public void LoadGameState()
	{
		if (!File.Exists(_filename))
		{
			Debug.Log("No saved game");
			return;
		}

		Dictionary<string, object> gameState;

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = File.Open(_filename, FileMode.Open);
		gameState = formatter.Deserialize(stream) as Dictionary<string, object>;
		stream.Close();
		
		Managers.Inventory.UpdateData((Dictionary<string, int>) gameState["inventory"]);
		Managers.Player.UpdateData((int) gameState["health"], (int) gameState["maxHealth"]);
		Managers.Mission.UpdateData((int) gameState["currentLevel"], (int) gameState["maxLevel"]);
		
		Managers.Mission.RestartCurrent();
	}
}
