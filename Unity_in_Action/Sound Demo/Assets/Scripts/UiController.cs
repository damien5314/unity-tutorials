using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
	[SerializeField] private Text _scoreLabel;
	[SerializeField] private SettingsMenu _settingsMenu;

	private int _score;

	private void Awake()
	{
		Messenger.AddListener(GameEvent.EnemyHit, OnEnemyHit);
	}

	private void OnDestroy()
	{
		Messenger.RemoveListener(GameEvent.EnemyHit, OnEnemyHit);
	}

	private void Start()
	{
		_score = 0;
		_scoreLabel.text = _score.ToString();

		_settingsMenu.Close();
	}

	public void OnOpenSettings()
	{
		_settingsMenu.Open();
	}

	private void OnEnemyHit()
	{
		_score += 1;
		_scoreLabel.text = _score.ToString();
	}
}
