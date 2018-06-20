using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public static GameManager Instance = null;

	public BoardManager BoardScript;
	public int PlayerFoodPoints = 100;
	[HideInInspector] public bool PlayersTurn = true;
	public float TurnDelay = 0.1f;
	public float LevelStartDelay = 2f;

	private int _level = 1;
	private List<Enemy> _enemies;
	private bool _enemiesMoving;
	private Text _levelText;
	private GameObject _levelImage;
	private bool _doingSetup;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

		_enemies = new List<Enemy>();
		BoardScript = GetComponent<BoardManager>();
		InitGame();
	}

	// TODO: Called by the Unity engine?? But it doesn't override anything, how do we know that?
	void OnLevelWasLoaded(int index)
	{
		_level++;
		InitGame();
	}

	private void InitGame()
	{
		_doingSetup = true;
		_levelImage = GameObject.Find("LevelImage");
		_levelText = GameObject.Find("LevelText").GetComponent<Text>();
		_levelText.text = "Day " + _level;
		_levelImage.SetActive(true);
		Invoke("HideLevelImage", LevelStartDelay);
		
		_enemies.Clear();
		BoardScript.SetupScene(_level);
	}

	private void HideLevelImage()
	{
		_levelImage.SetActive(false);
		_doingSetup = false;
	}

	public void GameOver()
	{
		enabled = false;
		_levelText.text = "After " + _level + " days, you starved.";
		_levelImage.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayersTurn || _enemiesMoving || _doingSetup)
		{
			return;
		}

		StartCoroutine(MoveEnemies());
	}

	public void AddEnemyToList(Enemy script)
	{
		_enemies.Add(script);
	}

	IEnumerator MoveEnemies()
	{
		_enemiesMoving = true;
		yield return new WaitForSeconds(TurnDelay);
		if (_enemies.Count == 0)
		{
			yield return new WaitForSeconds(TurnDelay);
		}

		for (int i = 0; i < _enemies.Count; i++)
		{
			_enemies[i].MoveEnemy();
			yield return new WaitForSeconds(_enemies[i].MoveTime);
		}

		PlayersTurn = true;
		_enemiesMoving = false;
	}
}
