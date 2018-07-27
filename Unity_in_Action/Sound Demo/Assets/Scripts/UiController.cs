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
		
		ShowSettingsMenu(false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			bool isShowing = _settingsMenu.gameObject.activeSelf;
			ShowSettingsMenu(!isShowing);
		}
	}

	private void ShowSettingsMenu(bool show)
	{
		_settingsMenu.gameObject.SetActive(show);

		if (show)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	private void OnEnemyHit()
	{
		_score += 1;
		_scoreLabel.text = _score.ToString();
	}
}
