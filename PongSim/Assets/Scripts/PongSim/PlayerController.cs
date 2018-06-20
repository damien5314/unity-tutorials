using System;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

namespace PongSim
{
	
	public class PlayerController : MonoBehaviour
	{

		public static float MOVE_DIST_PER_FRAME = 0.01f;

		public Boundary YBoundary;

		private Rigidbody2D _rigidbody;
		private BoxCollider2D _boxCollider;
		private LayerMask _collisionLayer;

		// Use this for initialization
		void Start ()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_boxCollider = GetComponent<BoxCollider2D>();
			_collisionLayer = gameObject.layer;
		}
	
		private void FixedUpdate()
		{
			ProcessInput();
		}

		private void ProcessInput()
		{
			int vertical = VerticalInput;
			if (vertical != 0)
			{
				TryMove(vertical);
			}
		}

		private int VerticalInput
		{
			get
			{
				float horizontal = Input.GetAxis("Vertical");
				if (Mathf.Abs(horizontal) > Mathf.Epsilon)
				{
					if (horizontal > 0) return 1;
					return -1;
				}
				return 0;
			}
		}

		private void TryMove(int dirY)
		{
			float moveY = MOVE_DIST_PER_FRAME * dirY;
			
			Vector2 transformPosition = transform.position;
			Vector2 newPos = transformPosition + new Vector2(0, moveY);
			Vector2 clamped = new Vector2(
				newPos.x,
				Mathf.Clamp(newPos.y, YBoundary.Min, YBoundary.Max)
			);
			
			_rigidbody.MovePosition(clamped);
		}
	}

	[System.Serializable]
	public class Boundary
	{
		public float Min;
		public float Max;
	}
}
