using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NPC : MonoBehaviour
{
	public Animator anim;

	public Transform followTarget = null;
	public Rigidbody rb;
	public float speed = 3.0f;
	public void SetTarget(Transform newTarget)
	{
		followTarget = newTarget;
	}
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();

		anim.SetBool("grounded", true);
	}

	private void FixedUpdate()
	{
		if (followTarget != null)
		{
			Vector3 diff = followTarget.position - transform.position;
			diff.y = 0.0f;
			float dist = diff.magnitude;

			if (dist > 0.5f)
			{
				Vector3 dir = diff.normalized;
				Vector3 v = dir * speed;
				v.y = rb.velocity.y;

				rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5.0f);
				rb.velocity = v;
			}
			else
			{
				rb.velocity = Vector3.Scale(rb.velocity, new Vector3(0.0f, 1.0f, 0.0f));
			}
		}
	}
}
