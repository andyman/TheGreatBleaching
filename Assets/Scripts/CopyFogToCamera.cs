using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CopyFogToCamera : MonoBehaviour
{
	public Camera cam;

	// Start is called before the first frame update
	void Start()
	{

	}

	private void OnEnable()
	{
		Refresh();
	}
	// Update is called once per frame
	void Update()
	{
		Refresh();
	}

	private void Refresh()
	{
		if (cam != null)
		{
			Color fogColor = RenderSettings.fogColor;
			cam.backgroundColor = fogColor;
		}
	}
}
