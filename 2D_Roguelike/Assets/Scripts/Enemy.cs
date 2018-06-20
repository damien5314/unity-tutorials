﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{

	public int PlayerDamage;
	public AudioClip EnemyAttack1;
	public AudioClip EnemyAttack2;

	private Animator _animator;
	private Transform _target;
	private bool _skipMove;
	
	// Use this for initialization
	protected override void Start ()
	{
		GameManager.Instance.AddEnemyToList(this);
		_animator = GetComponent<Animator>();
		_target = GameObject.FindGameObjectWithTag("Player").transform;
		base.Start();
	}

	protected override void AttemptMove<T>(int xDir, int yDir)
	{
		if (_skipMove)
		{
			_skipMove = false;
			return;
		}
		
		base.AttemptMove<T>(xDir, yDir);

		_skipMove = true;
	}

	public void MoveEnemy()
	{
		int xDir = 0;
		int yDir = 0;

		if (Math.Abs(_target.position.x - transform.position.x) < float.Epsilon)
		{
			yDir = _target.position.y > transform.position.y ? 1 : -1;
		}
		else // if (Math.Abs(_target.position.y - transform.position.y) < float.Epsilon) // Probably don't need the extra check?
		{
			xDir = _target.position.x > transform.position.x ? 1 : -1;
		}
		
		AttemptMove<Player>(xDir, yDir);
	}

	protected override void OnCantMove<T>(T component)
	{
		Player hitPlayer = component as Player;
		hitPlayer.LoseFood(PlayerDamage);

		_animator.SetTrigger("enemyAttack");
		
		SoundManager.Instance.RandomizeSfx(EnemyAttack1, EnemyAttack2);
	}
}
