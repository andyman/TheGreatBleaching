using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	private Camera cam;
	public bool invert = false;

	// Update is called once per frame
	void LateUpdate()
	{
		if (cam == null || !cam.gameObject.activeInHierarchy)
		{
			cam = Camera.main;
		}
		if (cam == null) return;

		if (!invert)
		{
			transform.LookAt(cam.transform);
		}
		else
		{
			transform.forward = (transform.position - cam.transform.position).normalized;
		}
	}
}
