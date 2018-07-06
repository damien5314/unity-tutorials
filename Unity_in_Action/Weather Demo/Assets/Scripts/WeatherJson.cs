using System;

[Serializable]
public struct WeatherJson
{
	public long id;
	public string name;
	public CloudData clouds;
}

[Serializable]
public struct CloudData
{
	public int all;
}
