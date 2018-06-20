using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

	public Sprite DmgSprite;
	public int HealthPoints = 4;

	public AudioClip ChopSound1;
	public AudioClip ChopSound2;

	private SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start ()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void DamageWall(int loss)
	{
		SoundManager.Instance.RandomizeSfx(ChopSound1, ChopSound2);
		
		_spriteRenderer.sprite = DmgSprite;
		HealthPoints -= loss;

		if (HealthPoints <= 0)
		{
			gameObject.SetActive(false);
		}
	}
}
