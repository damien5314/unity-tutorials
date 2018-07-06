using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager {
	
	public ManagerStatus Status { get; private set; }
	
	// Add cloud value here (listing 9.8)
	private NetworkService _network;
	
	public void Startup(NetworkService service)
	{
		Debug.Log("Weather manager starting...");
		
		_network = service;
		StartCoroutine(_network.GetWeatherXml(OnXmlDataLoaded));

		Status = ManagerStatus.Initializing;
	}

	public void OnXmlDataLoaded(string data)
	{
		Debug.Log(data);

		Status = ManagerStatus.Started;
	}
}
