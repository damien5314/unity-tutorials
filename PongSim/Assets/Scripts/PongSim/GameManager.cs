using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PongSim
{

	public class GameManager : MonoBehaviour
	{

		public static GameManager Instance;

		private void InitGameObject()
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
		}

		// Use this for initialization
		void Start () {
			InitGameObject();
		}
	}

}
