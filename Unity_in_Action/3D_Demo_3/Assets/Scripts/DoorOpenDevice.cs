using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{

	[SerializeField] private Vector3 _doorPosition;

	private bool _open = false;

	public void Operate()
	{
		if (_open)
		{
			// TODO: Use tween to animate the opening & closing of the door
			Vector3 position = transform.position - _doorPosition;
			transform.position = position;
		}
		else
		{
			Vector3 position = transform.position + _doorPosition;
			transform.position = position;
		}

		_open = !_open;
	}

	public void Activate()
	{
		if (!_open)
		{
			Vector3 position = transform.position + _doorPosition;
			transform.position = position;
			_open = true;
		}
	}

	public void Deactivate()
	{
		if (_open)
		{
			Vector3 position = transform.position - _doorPosition;
			transform.position = position;
			_open = false;
		}
	}
}
