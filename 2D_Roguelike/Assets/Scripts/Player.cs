using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MovingObject
{

	public int WallDamage = 1;
	public int PointsPerFood = 10;
	public int PointsPerSoda = 20;
	public float RestartLevelDelay = 1;
	public Text FoodText;

	public AudioClip MoveSound1;
	public AudioClip MoveSound2;
	public AudioClip EatSound1;
	public AudioClip EatSound2;
	public AudioClip DrinkSound1;
	public AudioClip DrinkSound2;
	public AudioClip GameOverSound;

	private Animator _animator;
	private int _food;
	private Vector2 _touchOrigin = -Vector2.one;

	// Use this for initialization
	protected override void Start ()
	{
		_animator = GetComponent<Animator>();
		_food = GameManager.Instance.PlayerFoodPoints;
		FoodText.text = "Food: " + _food;
		
		base.Start();
	}

	private void OnDisable()
	{
		GameManager.Instance.PlayerFoodPoints = _food;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!GameManager.Instance.PlayersTurn) return;

		int horizontal = 0;
		int vertical = 0;
		
	#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER

		horizontal = (int) Input.GetAxisRaw("Horizontal");
		vertical = (int) Input.GetAxisRaw("Vertical");

		if (horizontal != 0)
		{
			vertical = 0;
		}
	
	#else

		if (Input.touchCount > 0)
		{
			Touch myTouch = Input.touches[0];

			if (myTouch.phase == TouchPhase.Began)
			{
				_touchOrigin = myTouch.position;
			}
			else if (myTouch.phase == TouchPhase.Ended && _touchOrigin.x >= 0)
			{
				Vector2 touchEnd = myTouch.position;
				float x = touchEnd.x - _touchOrigin.x;
				float y = touchEnd.y - _touchOrigin.y;
				_touchOrigin.x = -1; // Reset the condition guarding this conditional

				if (Mathf.Abs(x) > Mathf.Abs(y))
				{
					horizontal = x > 0 ? 1 : -1;
				}
				else
				{
					vertical = y > 0 ? 1 : -1;
				}
			}
		}
		
	#endif

		if (horizontal != 0 || vertical != 0)
		{
			AttemptMove<Wall>(horizontal, vertical);
		}
	}

	protected override void AttemptMove<T>(int xDir, int yDir)
	{
		_food--;
		FoodText.text = "Food: " + _food;
		
		base.AttemptMove<T>(xDir, yDir);

		RaycastHit2D hit;

		if (Move(xDir, yDir, out hit))
		{
			SoundManager.Instance.RandomizeSfx(MoveSound1, MoveSound2);
		}
		
		CheckIfGameOver();

		GameManager.Instance.PlayersTurn = false;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Exit")
		{
			Invoke("Restart", RestartLevelDelay);
			enabled = false;
		}
		else if (other.tag == "Food")
		{
			_food += PointsPerFood;
			FoodText.text = "+" + PointsPerFood + " Food: " + _food;
			other.gameObject.SetActive(false);
			SoundManager.Instance.RandomizeSfx(EatSound1, EatSound2);
		}
		else if (other.tag == "Soda")
		{
			_food += PointsPerSoda;
			FoodText.text = "+" + PointsPerSoda + " Food: " + _food;
			other.gameObject.SetActive(false);
			SoundManager.Instance.RandomizeSfx(DrinkSound1, DrinkSound2);
		}
	}

	protected override void OnCantMove<T>(T component)
	{
		Wall hitWall = component as Wall;
		hitWall.DamageWall(WallDamage);
		_animator.SetTrigger("playerChop");
	}

	private void Restart()
	{
		SceneManager.LoadScene(0);
	}

	public void LoseFood(int loss)
	{
		_animator.SetTrigger("playerHit");
		_food -= loss;
		FoodText.text = "-" + loss + " Food: " + _food;
		CheckIfGameOver();
	}

	private void CheckIfGameOver()
	{
		if (_food <= 0)
		{
			SoundManager.Instance.PlaySingle(GameOverSound);
			SoundManager.Instance.MusicSource.Stop();
			
			GameManager.Instance.GameOver();
		}
	}
}
