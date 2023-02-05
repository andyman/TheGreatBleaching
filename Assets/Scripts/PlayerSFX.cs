using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class PlayerSFX : MonoBehaviour
{

	private float nextPlayableTime;

	PlayerWalkController player;
	private void Awake()
	{
		player = GetComponentInParent<PlayerWalkController>();

	}

	private void UpdateSound()
	{
		if (Time.time > nextPlayableTime)
		{
			RuntimeManager.PlayOneShot(FMODEvents.instance.playerFootsteps, transform.position);
			nextPlayableTime = Time.time + 0.15f;
		}
	}

	private void PlayerLandedSFX()
	{
		Debug.Log("Play landing");
		RuntimeManager.PlayOneShot(FMODEvents.instance.playerLanding, transform.position);
		player.landedJump = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		int otherLayer = other.gameObject.layer;

		if (otherLayer == 6 || otherLayer == 7)
		{
			Debug.Log(player.landedJump);
			if (player.landedJump)
			{
				PlayerLandedSFX();
			}
			else
			{
				UpdateSound();
			}

		}
	}


}
