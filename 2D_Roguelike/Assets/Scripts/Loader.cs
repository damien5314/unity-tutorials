using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{

	public GameObject GameManager;

	// Use this for initialization
	void Awake () {
		if (global::GameManager.Instance == null)
		{
			Instantiate(GameManager);
		}
	}
}
