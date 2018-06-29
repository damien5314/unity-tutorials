using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{

	[SerializeField] private Text _scoreLabel;
	
	private void Update()
	{
		_scoreLabel.text = Time.realtimeSinceStartup.ToString(CultureInfo.CurrentCulture);
	}

	public void OnOpenSettings()
	{
		Debug.Log("Opened settings");
	}
}
