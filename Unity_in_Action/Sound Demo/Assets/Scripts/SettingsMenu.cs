using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

	public void OnSoundToggle()
	{
		GameManagers.Audio.SoundMute = !GameManagers.Audio.SoundMute;
	}

	public void OnSoundValue(float volume)
	{
		GameManagers.Audio.SoundVolume = volume;
	}
}
