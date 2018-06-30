using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

	[SerializeField] private InputField _nameField;
	[SerializeField] private Slider _speedSlider;

	private void Start()
	{
		_nameField.text = SettingsManager.PlayerName;
		_speedSlider.value = SettingsManager.Speed;
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
		SettingsManager.PlayerName = submitted;
	}

	public void OnSpeedValue(float speed)
	{
		SettingsManager.Speed = speed;
	}
}
