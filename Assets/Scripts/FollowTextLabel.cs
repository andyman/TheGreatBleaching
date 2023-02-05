using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class FollowTextLabel : MonoBehaviour
{

	public Transform target;
	public Vector3 worldOffset;
	public Vector3 pixelOffset;

	private Transform cameraTransform;
	private Camera cam;


	// Use this for initialization
	void OnEnable()
	{
		cam = Camera.main;

		cameraTransform = cam.transform;

		RefreshTransform();
	}

	void RefreshTransform()
	{
		if (target == null) return;
		if (cam == null) cam = Camera.main;
		if (cam == null) return;

		Vector3 screenPos = cam.WorldToScreenPoint(target.position + worldOffset) + pixelOffset;

		transform.position = Vector3.Lerp(transform.position, screenPos, Time.deltaTime * 10.0f);

		//transform.position = target.position + offset;
		//transform.rotation = Quaternion.LookRotation((transform.position - cameraTransform.position).normalized);
	}

	private void Update()
	{
		RefreshTransform();
	}

	// Update is called once per frame
	void LateUpdate()
	{
		RefreshTransform();
	}
}
