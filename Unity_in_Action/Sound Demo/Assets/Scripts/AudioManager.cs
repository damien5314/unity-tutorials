using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
	public ManagerStatus Status { get; private set; }

	[SerializeField] private AudioSource _soundSource;
	[SerializeField] private AudioSource _music1Source;
	[SerializeField] private AudioSource _music2Source;
	[SerializeField] private string _introBgMusic;
	[SerializeField] private string _levelBgMusic;

	public float CrossfadeRate = 1.5f;

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

			if (_music1Source != null && !_crossfading)
			{
				_music1Source.volume = _musicVolume;
				_music2Source.volume = _musicVolume;
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
				_music2Source.mute = value;
			}
		}
	}

	private NetworkService _network;
	private AudioSource _activeMusic;
	private AudioSource _inactiveMusic;
	private bool _crossfading = false;

	public void Startup(NetworkService service)
	{
		Debug.Log("Audio manager starting...");

		_network = service;

		SoundVolume = 1.0f;
		MusicVolume = 1.0f;

		_music1Source.ignoreListenerVolume = true;
		_music1Source.ignoreListenerPause = true;
		_music2Source.ignoreListenerVolume = true;
		_music2Source.ignoreListenerPause = true;

		_activeMusic = _music1Source;
		_inactiveMusic = _music2Source;

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
		if (_crossfading) return;

		StartCoroutine(CrossfadeMusic(audioClip));
	}

	private IEnumerator CrossfadeMusic(AudioClip clip)
	{
		_crossfading = true;

		_inactiveMusic.clip = clip;
		_inactiveMusic.volume = 0;
		_inactiveMusic.Play();

		float scaledRate = CrossfadeRate * _musicVolume;
		while (_activeMusic.volume > 0)
		{
			_activeMusic.volume -= scaledRate * Time.deltaTime;
			_inactiveMusic.volume += scaledRate * Time.deltaTime;

			yield return null;
		}

		AudioSource temp = _activeMusic;

		_activeMusic = _inactiveMusic;
		_activeMusic.volume = _musicVolume;

		_inactiveMusic = temp;
		_inactiveMusic.Stop();

		_crossfading = false;
	}

	public void StopMusic()
	{
		_activeMusic.Stop();
		_inactiveMusic.Stop();
	}
}
