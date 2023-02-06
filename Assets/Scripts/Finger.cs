using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : MonoBehaviour
{
	public ParticleSystem ps;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<DestroyableHouse>() != null)
		{
			collision.gameObject.SetActive(false);
			Vector3 collisionPoint = collision.contacts[0].point;
			ps.transform.position = collisionPoint;
			ps.Emit(10);
		}

	}
	// Start is called before the first frame update
	void Start()
	{

	}

}
