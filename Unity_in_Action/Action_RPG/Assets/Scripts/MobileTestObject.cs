using UnityEngine;

public class MobileTestObject : MonoBehaviour
{
	private string _message;

	private void Awake()
	{
		TestPlugin.Initialize();
	}
	
	private void Start()
	{
		_message = "START: " + TestPlugin.TestString("ThIs Is A tEsT");
	}

	private void Update()
	{
		// Make sure user touched the screen
		if (Input.touchCount == 0) return;

		Touch touch = Input.GetTouch(0);
		if (touch.phase == TouchPhase.Began)
		{
			_message = "TOUCH: " + TestPlugin.TestNumber();
		}
	}

	private void OnGUI()
	{
		GUI.contentColor = Color.black;
		GUI.Label(new Rect(10, 10, 200, 20), _message);
	}
}
