using System.Collections;
using System.Security.Policy;
using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager {

	public const string WeatherEndpoint =
		"https://api.openweathermap.org/data/2.5/weather?q=Osaka&appid=c54203e013cb940efbc7d5a2fcc46b1b";
	
	public ManagerStatus Status { get; private set; }

	public float CloudValue { get; private set; }

	private NetworkService _network;
	
	public void Startup(NetworkService service)
	{
		Debug.Log("Weather manager starting...");
		
		_network = service;
		StartCoroutine(GetWeatherData());

		Status = ManagerStatus.Initializing;
	}

	private IEnumerator GetWeatherData()
	{
		return _network.CallApi(WeatherEndpoint, OnWeatherDataLoaded);
	}

	private void OnWeatherDataLoaded(string data)
	{
		WeatherJson weatherData = JsonUtility.FromJson<WeatherJson>(data);
		CloudValue = weatherData.clouds.all / 100f;
		Messenger.Broadcast(GameEvent.WeatherUpdated);

		Status = ManagerStatus.Started;
	}
}
