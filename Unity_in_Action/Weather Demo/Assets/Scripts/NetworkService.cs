using System;
using System.Collections;
using UnityEngine;

public class NetworkService
{

	public const string WeatherEndpoint =
		"https://api.openweathermap.org/data/2.5/weather?q=Chicago,us&appid=c54203e013cb940efbc7d5a2fcc46b1b";

	private bool IsResponseValid(WWW www)
	{
		if (www.error != null)
		{
			Debug.Log("Bad connection");
			return false;
		}

		if (string.IsNullOrEmpty(www.text))
		{
			Debug.Log("Bad data");
			return false;
		}

		return true;
	}

	private IEnumerator CallApi(string url, Action<string> callback)
	{
		WWW www = new WWW(url);
		yield return www;

		if (!IsResponseValid(www)) yield break;

		callback(www.text);
	}

	public IEnumerator GetWeatherXml(Action<string> callback)
	{
		return CallApi(WeatherEndpoint, callback);
	}
}
