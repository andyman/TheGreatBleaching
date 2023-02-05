using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueTrigger : MonoBehaviour
{
	public LayerMask interestedLayerMask;

	public bool runOnce = true;
	public bool ran = false;

	public DialogueLine[] lines;

	// Start is called before the first frame update
	void Start()
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		if ((1 << other.gameObject.layer & interestedLayerMask) == 0) return;
		if (runOnce && ran) return;

		DialogueThang thang = other.GetComponentInChildren<DialogueThang>();
		thang.ShutUp();

		thang.lines = lines;
		thang.StartTalking();

		ran = true;
		if (runOnce)
		{
			gameObject.SetActive(false);
		}
	}
}
