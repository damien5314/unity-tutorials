using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour, IGameManager 
{
	
	public ManagerStatus Status { get; private set; }

	public int CurrentLevel { get; private set; }
	public int MaxLevel { get; private set; }

	private NetworkService _network;
	
	public void Startup(NetworkService service)
	{
		Debug.Log("Mission manager starting...");

		_network = service;

		CurrentLevel = 0;
		MaxLevel = 1;
		
		Status = ManagerStatus.Started;
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
