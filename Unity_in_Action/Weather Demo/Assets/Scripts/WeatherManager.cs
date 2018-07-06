using System.Collections;
using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager {

	public const string WeatherEndpoint =
		"https://api.openweathermap.org/data/2.5/weather?q=Chicago,us&appid=c54203e013cb940efbc7d5a2fcc46b1b";
	
	public ManagerStatus Status { get; private set; }
	
	// Add cloud value here (listing 9.8)
	private NetworkService _network;
	
	public void Startup(NetworkService service)
	{
		Debug.Log("Weather manager starting...");
		
		_network = service;
		StartCoroutine(GetWeatherXml());

		Status = ManagerStatus.Initializing;
	}

	private IEnumerator GetWeatherXml()
	{
		return _network.CallApi(WeatherEndpoint, OnXmlDataLoaded);
	}

	private void OnXmlDataLoaded(string data)
	{
		Debug.Log(data);

		Status = ManagerStatus.Started;
	}
}
