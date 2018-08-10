﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{

	[SerializeField] private Text _healthLabel;
	[SerializeField] private InventoryPopup _popup;
	[SerializeField] private Text _levelEnding;

	private void Awake()
	{
		Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
		Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
		Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
		Messenger.AddListener(GameEvent.GAME_COMPLETE, OnGameComplete);
	}

	private void OnDestroy()
	{
		Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
		Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
		Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
		Messenger.RemoveListener(GameEvent.GAME_COMPLETE, OnGameComplete);
	}

	private void Start()
	{
		OnHealthUpdated();

		_levelEnding.gameObject.SetActive(false);
		_popup.gameObject.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			bool isShowing = _popup.gameObject.activeSelf;
			_popup.gameObject.SetActive(!isShowing);
			_popup.Refresh();
		}
	}

	private void OnHealthUpdated()
	{
		string message = "Health: " + Managers.Player.health + "/" + Managers.Player.maxHealth;
		_healthLabel.text = message;
	}

	private void OnLevelComplete()
	{
		StartCoroutine(CompleteLevel());
	}

	private IEnumerator CompleteLevel()
	{
		_levelEnding.gameObject.SetActive(true);
		_levelEnding.text = "Level Complete!";

		yield return new WaitForSeconds(2);
		
		Managers.Mission.GoToNext();
	}

	private void OnLevelFailed()
	{
		StartCoroutine(FailLevel());
	}

	private IEnumerator FailLevel()
	{
		_levelEnding.gameObject.SetActive(true);
		_levelEnding.text = "Level Failed";

		yield return new WaitForSeconds(2);
		
		Managers.Player.Respawn();
		Managers.Mission.RestartCurrent();
	}

	private void OnGameComplete()
	{
		_levelEnding.gameObject.SetActive(true);
		_levelEnding.text = "You finished the game!";
	}

	public void SaveGame()
	{
		Managers.Data.SaveGameState();
	}

	public void LoadGame()
	{
		Managers.Data.LoadGameState();
	}
}
