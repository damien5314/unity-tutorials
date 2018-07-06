using System;
using UnityEngine;

public class ImagesManager : MonoBehaviour, IGameManager
{

	private const string ImageUrl = "https://i.imgur.com/wNgCY3f.jpg";
	
	public ManagerStatus Status { get; private set; }

	private NetworkService _network;
	private Texture2D _webImage;
	
	public void Startup(NetworkService service)
	{
		Debug.Log("Images manager starting...");

		_network = service;

		Status = ManagerStatus.Started;
	}

	public void GetWebImage(Action<Texture2D> callback)
	{
		if (_webImage == null)
		{
			StartCoroutine(_network.DownloadImage(ImageUrl, imageTexture =>
			{
				_webImage = imageTexture;
				callback(imageTexture);
			}));
		}
		else
		{
			callback(_webImage);
		}
	}
}
