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

	// Add volume controls here (listing 10.4)

	public void Startup(NetworkService service)
	{
		Debug.Log("Audio manager starting...");

		_network = service;

		// Initialize music sources here (listing 10.10)

		Status = ManagerStatus.Started;
	}
}
