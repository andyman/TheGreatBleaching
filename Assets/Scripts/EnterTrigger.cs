using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnterTrigger : MonoBehaviour
{
	public UnityEvent enterEvent;

	private void OnTriggerEnter(Collider other)
	{
		enterEvent.Invoke();
	}
}
