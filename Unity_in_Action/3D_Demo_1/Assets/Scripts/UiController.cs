using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{

	[SerializeField] private Text _scoreLabel;
	[SerializeField] private SettingsMenu _settingsMenu;

	private void Start()
	{
		_settingsMenu.Close();
	}

	private void Update()
	{
		_scoreLabel.text = Time.realtimeSinceStartup.ToString(CultureInfo.CurrentCulture);
	}

	public void OnOpenSettings()
	{
		_settingsMenu.Open();
	}
}
