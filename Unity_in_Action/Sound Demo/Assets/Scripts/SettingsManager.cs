using UnityEngine;

public static class SettingsManager
{

	private const string PrefsName = "prefs_name";
	private const string PrefsSpeed = "prefs_speed";

	public static string PlayerName
	{
		get { return PlayerPrefs.GetString(PrefsName); }
		set { PlayerPrefs.SetString(PrefsName, value); }
	}

	public static float Speed
	{
		get { return PlayerPrefs.GetFloat(PrefsSpeed); }
		set
		{
			PlayerPrefs.SetFloat(PrefsSpeed, value);
			Messenger<float>.Broadcast(GameEvent.SpeedChanged, value);
		}
	}
}
