using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
	public ManagerStatus Status { get; private set; }

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
}
