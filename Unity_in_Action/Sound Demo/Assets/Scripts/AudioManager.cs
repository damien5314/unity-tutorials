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

	private float _musicVolume;

	public float MusicVolume
	{
		get { return _musicVolume; }
		set
		{
			_musicVolume = value;

			if (_music1Source != null)
			{
				_music1Source.volume = _musicVolume;
			}
		}
	}

	public bool MusicMute
	{
		get
		{
			if (_music1Source != null)
			{
				return _music1Source.mute;
			}
			return false;
		}
		set
		{
			if (_music1Source != null)
			{
				_music1Source.mute = value;
			}
		}
	}

	private NetworkService _network;

	[SerializeField] private AudioSource _soundSource;

	public void Startup(NetworkService service)
	{
		Debug.Log("Audio manager starting...");

		_network = service;

		SoundVolume = 1.0f;
		MusicVolume = 1.0f;

		_music1Source.ignoreListenerVolume = true;
		_music1Source.ignoreListenerPause = true;

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
