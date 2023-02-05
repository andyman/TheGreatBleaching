using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceWhenInRange : MonoBehaviour
{
	public LayerMask interestedLayer;
	public float turnRate = 90.0f;

	public Transform thingToTurn;
	public float turnBackDuration = 2.0f;
	private float faceUntilTime;

	private Quaternion initialRotation;

	// Start is called before the first frame update
	void Start()
	{
		initialRotation = thingToTurn.rotation;

	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time > faceUntilTime)
		{
			thingToTurn.rotation = Quaternion.RotateTowards(thingToTurn.rotation, initialRotation, turnRate * Time.deltaTime);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		int bitMask = 1 << other.gameObject.layer;

		//bitwise AND
		if ((bitMask & interestedLayer) != 0)
		{
			Vector3 otherPos = other.transform.position;
			otherPos.y = thingToTurn.position.y;

			Vector3 dir = (otherPos - thingToTurn.position).normalized;
			Quaternion newRot = Quaternion.LookRotation(dir);
			thingToTurn.rotation = Quaternion.RotateTowards(thingToTurn.rotation, newRot, turnRate * Time.deltaTime * Random.Range(0.5f, 1.0f));
			faceUntilTime = Time.time + turnBackDuration;

		}
	}


}
