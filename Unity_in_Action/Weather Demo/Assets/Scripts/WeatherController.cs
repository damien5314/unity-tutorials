using System;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
	
	[SerializeField] private Material _sky;
	[SerializeField] private Light _sun;

	private float _fullIntensity;

	private void Awake()
	{
		Messenger.AddListener(GameEvent.WeatherUpdated, OnWeatherUpdated);
	}

	private void OnDestroy()
	{
		Messenger.RemoveListener(GameEvent.WeatherUpdated, OnWeatherUpdated);
	}

	private void Start()
	{
		_fullIntensity = _sun.intensity;
	}

	private void OnWeatherUpdated()
	{
		SetOvercast(GameManagers.Weather.CloudValue);
	}

	private void SetOvercast(float value)
	{
//		_sky.SetFloat("_Blend", value);
		_sky.SetFloat("_Blend", Math.Min(1, value));
		_sun.intensity = _fullIntensity - (_fullIntensity * value);
	}
}
