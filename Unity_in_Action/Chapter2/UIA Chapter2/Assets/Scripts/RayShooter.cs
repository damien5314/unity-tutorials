using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{

	private Camera _camera;

	// Use this for initialization
	void Start ()
	{
		_camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 point = new Vector3(_camera.pixelWidth / 2f, _camera.pixelHeight / 2f, 0);
			Ray ray = _camera.ScreenPointToRay(point);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				StartCoroutine(SphereIndicator(hit.point));
			}
		}
	}

	private IEnumerator SphereIndicator(Vector3 point)
	{
		Debug.Log("Hit " + point);

		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.position = point;

		yield return new WaitForSeconds(1);

		Destroy(sphere);
	}
}
