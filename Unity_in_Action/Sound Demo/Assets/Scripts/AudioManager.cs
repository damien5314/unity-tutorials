using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
	public ManagerStatus Status { get; private set; }

	[SerializeField] private AudioSource _music1Source;
	[SerializeField] private string _introBgMusic;
	[SerializeField] private string _levelBgMusic;

	public float SoundVolume
	{
		get { return AudioListener.volume; }
		set { AudioListener.volume = value; }
	}

	public bool SoundMute
	{
		get { return AudioListener.pause; }
		set { AudioListener.pause = value; }
	}

	private NetworkService _network;

	[SerializeField] private AudioSource _soundSource;

	public void Startup(NetworkService service)
	{
		Debug.Log("Audio manager starting...");

		_network = service;

		SoundVolume = 1.0f;

		// Initialize music sources here (listing 10.10)

		Status = ManagerStatus.Started;
	}

	public void PlaySound(AudioClip clip)
	{
		_soundSource.PlayOneShot(clip);
	}

	public void PlayIntroMusic()
	{
		PlayMusic(Resources.Load("Music/" + _introBgMusic) as AudioClip);
	}

	public void PlayLevelMusic()
	{
		PlayMusic(Resources.Load("Music/" + _levelBgMusic) as AudioClip);
	}

	private void PlayMusic(AudioClip audioClip)
	{
		_music1Source.clip = audioClip;
		_music1Source.Play();
	}

	public void StopMusic()
	{
		_music1Source.Stop();
	}
}
