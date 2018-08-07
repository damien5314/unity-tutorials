using System;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
	[SerializeField] private AudioClip _sound;

	public void OnSoundToggle()
	{
		GameManagers.Audio.SoundMute = !GameManagers.Audio.SoundMute;
		GameManagers.Audio.PlaySound(_sound);
	}

	public void OnSoundValue(float volume)
	{
		GameManagers.Audio.SoundVolume = volume;
	}

	public void OnPlayMusic(int selector)
	{
		GameManagers.Audio.PlaySound(_sound);

		switch (selector)
		{
			case 1:
				GameManagers.Audio.PlayIntroMusic();
				break;
			case 2:
				GameManagers.Audio.PlayLevelMusic();
				break;
			case 3:
				GameManagers.Audio.StopMusic();
				break;
			default:
				throw new ArgumentException("Unexpected selector: " + selector);
		}
	}

	public void OnMusicToggle()
	{
		GameManagers.Audio.MusicMute = !GameManagers.Audio.MusicMute;
		GameManagers.Audio.PlaySound(_sound);
	}

	public void OnMusicValue(float volume)
	{
		GameManagers.Audio.MusicVolume = volume;
	}
}
