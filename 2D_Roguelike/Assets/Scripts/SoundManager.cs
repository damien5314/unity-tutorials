﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

	public static SoundManager Instance = null;

	public AudioSource EfxSource;
	public AudioSource MusicSource;

	public float LowPitchRange = 0.95f;
	public float HighPitchRange = 1.05f;

	// Use this for initialization
	void Awake ()
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

	public void PlaySingle(AudioClip clip)
	{
		EfxSource.clip = clip;
		EfxSource.Play();
	}

	public void RandomizeSfx(params AudioClip[] clips)
	{
		int randomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

		EfxSource.pitch = randomPitch;
		EfxSource.clip = clips[randomIndex];
		EfxSource.Play();
	}
}
