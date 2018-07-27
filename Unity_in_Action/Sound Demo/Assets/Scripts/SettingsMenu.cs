using UnityEngine;
using UnityEngine.UI;

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
}
