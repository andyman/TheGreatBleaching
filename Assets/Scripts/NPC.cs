using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
	public Animator anim;

	// Start is called before the first frame update
	void Start()
	{
		anim.SetBool("grounded", true);
	}

}
