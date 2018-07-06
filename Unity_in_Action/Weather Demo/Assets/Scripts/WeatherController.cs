using System;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
	
	private const float CloudIncrement = 0.005f;

	[SerializeField] private Material _sky;
	[SerializeField] private Light _sun;

	private float _fullIntensity;
	private float _cloudValue = 0f;

	private void Start()
	{
		_fullIntensity = _sun.intensity;
	}

	private void Update()
	{
		SetOvercast(_cloudValue);
		_cloudValue += CloudIncrement;
	}

	private void SetOvercast(float value)
	{
//		_sky.SetFloat("_Blend", value);
		_sky.SetFloat("_Blend", Math.Min(1, value));
		_sun.intensity = _fullIntensity - (_fullIntensity * value);
	}
}
