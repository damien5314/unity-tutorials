using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour
{
	private Camera _camera;

	// Use this for initialization
	void Start()
	{
		_camera = GetComponent<Camera>();

		// Hides the mouse cursor at the center of the screen
		// Removing to allow usage of the GUI
//		Cursor.lockState = CursorLockMode.Locked;
//		Cursor.visible = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			Vector3 point = new Vector3(_camera.pixelWidth / 2f, _camera.pixelHeight / 2f, 0);
			Ray ray = _camera.ScreenPointToRay(point);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				GameObject hitObject = hit.transform.gameObject;
				ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
				if (target != null)
				{
					target.ReactToHit();
				}
				else
				{
					StartCoroutine(SphereIndicator(hit.point));
				}
			}
		}
	}

	private IEnumerator SphereIndicator(Vector3 point)
	{
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.position = point;

		yield return new WaitForSeconds(1);

		Destroy(sphere);
	}

	void OnGUI()
	{
		int size = 12;
		float positionX = _camera.pixelWidth / 2 - size / 4;
		float positionY = _camera.pixelHeight / 2 - size / 4;
		GUI.Label(new Rect(positionX, positionY, size, size), "*");
	}
}