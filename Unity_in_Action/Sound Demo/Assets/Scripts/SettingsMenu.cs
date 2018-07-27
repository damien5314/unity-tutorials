using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	private void Start()
	{
	}

	public void Open()
	{
		gameObject.SetActive(true);
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}
}
