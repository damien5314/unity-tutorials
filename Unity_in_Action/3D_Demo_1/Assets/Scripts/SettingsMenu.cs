using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

	private const string PrefsName = "prefs_name";
	private const string PrefsSpeed = "prefs_speed";

	[SerializeField] private InputField _nameField;
	[SerializeField] private Slider _speedSlider;

	private void Start()
	{
		_nameField.text = PlayerPrefs.GetString(PrefsName);
		_speedSlider.value = PlayerPrefs.GetFloat(PrefsSpeed);
	}

	public void Open()
	{
		gameObject.SetActive(true);
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}

	public void OnSubmitName(string submitted)
	{
		PlayerPrefs.SetString(PrefsName, submitted);
	}

	public void OnSpeedValue(float speed)
	{
		PlayerPrefs.SetFloat(PrefsSpeed, speed);
		Messenger<float>.Broadcast(GameEvent.SpeedChanged, speed);
	}
}
