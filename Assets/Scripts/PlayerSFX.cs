using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class PlayerSFX : MonoBehaviour
{

	private float nextPlayableTime;


	private void UpdateSound()
	{
		if (Time.time > nextPlayableTime)
		{
			RuntimeManager.PlayOneShot(FMODEvents.instance.playerFootsteps, transform.position);
			nextPlayableTime = Time.time + 0.15f;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		int otherLayer = other.gameObject.layer;

		if (otherLayer == 6 || otherLayer == 7)
		{
			UpdateSound();
		}
	}


}
