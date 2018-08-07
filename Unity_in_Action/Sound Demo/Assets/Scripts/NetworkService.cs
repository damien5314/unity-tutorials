using System;
using System.Collections;
using UnityEngine;

public class NetworkService
{
	private bool IsResponseValid(WWW www)
	{
		if (www.error != null)
		{
			Debug.Log("Bad connection");
			return false;
		}

		if (string.IsNullOrEmpty(www.text))
		{
			Debug.Log("Bad data");
			return false;
		}

		return true;
	}

	public IEnumerator CallApi(string url, Action<string> callback)
	{
		WWW www = new WWW(url);
		yield return www;

		if (!IsResponseValid(www)) yield break;

		callback(www.text);
	}

	public IEnumerator DownloadImage(string url, Action<Texture2D> callback)
	{
		WWW www = new WWW(url);
		yield return www;

		// FIXME: Why don't we need error handling here?
		callback(www.texture);
	}
}
