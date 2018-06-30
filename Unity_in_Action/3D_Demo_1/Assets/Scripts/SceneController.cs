using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

	[SerializeField] private GameObject _enemyPrefab;
	private GameObject _enemy;

	void Update()
	{
		if (_enemy == null)
		{
			_enemy = Instantiate(_enemyPrefab);
			_enemy.transform.position = new Vector3(8, 1, -20);
			float angle = Random.Range(0, 360);
			_enemy.transform.Rotate(0, angle, 0);
		}
	}
}
