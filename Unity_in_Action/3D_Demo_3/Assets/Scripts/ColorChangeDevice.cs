using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeDevice : MonoBehaviour {

	public void Operate()
	{
		Color random = new Color(
			r: Random.Range(0f, 1f),
			g: Random.Range(0f, 1f),
			b: Random.Range(0f, 1f)
		);

		GetComponent<Renderer>().material.color = random;
	}
}
