using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager {
	
	public ManagerStatus Status { get; private set; }
	
	// Add cloud value here (listing 9.8)
	private NetworkService _network;
	
	public void Startup(NetworkService service)
	{
		Debug.Log("Weather manager starting...");
		
		_network = service;

		Status = ManagerStatus.Started;
	}
}
