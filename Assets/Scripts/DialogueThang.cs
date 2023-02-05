using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using FMODUnity;
using FMOD.Studio;

[System.Serializable]
public class DialogueLine
{
	[Multiline]
	public string text;

	public float durationAfterTyping = 2.0f;
	public UnityEvent eventAfterSpeaking;
	public UnityEvent eventBeforeSpeaking;

}

public class DialogueThang : MonoBehaviour
{
	public LayerMask interestedLayerMask;

	public DialogueLine[] lines;

	private int currentLine = -1;

	public TextMeshProUGUI txt;

	public float keyDelay = 0.1f;

	public UnityEvent doneEvent;

	private bool skipTriggered = false;

	public GameObject visualObjectContainer;

	public string audioEventPath;
	public bool triggerable = true;
	public bool skippable = false;

	void Awake()
	{
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (skippable)
		{
			skipTriggered = skipTriggered || Input.GetButtonDown("Jump");
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!triggerable) return;

		if ((1 << other.gameObject.layer & interestedLayerMask) == 0) return;

		if (currentLine == -1)
		{
			StartTalking();
		}
	}

	public void StartTalking()
	{
		skipTriggered = false;
		ShutUp();
		visualObjectContainer.transform.parent = null;
		visualObjectContainer.SetActive(true);
		StartCoroutine(RunLines());
	}

	public void ShutUp()
	{
		visualObjectContainer.SetActive(false);
		visualObjectContainer.transform.parent = transform;
		doneEvent.Invoke();
		currentLine = -1;
		StopAllCoroutines();
	}
	IEnumerator RunLines()
	{
		for (currentLine = 0; currentLine < lines.Length; currentLine++)
		{
			DialogueLine line = lines[currentLine];
			yield return StartCoroutine(Run(line));
		}
		ShutUp();

	}

	IEnumerator Run(DialogueLine line)
	{
		float nextPlayTime = 0;

		txt.text = "";
		line.eventBeforeSpeaking.Invoke();
		Transform camTran = Camera.main.transform;
		string story = line.text;

		float startTime = Time.time;
		foreach (char c in story)
		{
			txt.text += c;
			if (c != ' ' && c != '\n' && Time.time >= nextPlayTime)
			{
				Vector3 pos = camTran.position + Vector3.forward;
				// TODO: play random sound at this position
				//
				RuntimeManager.PlayOneShot(audioEventPath, transform.position);
				nextPlayTime = Time.time + 0.2f;
			}

			yield return new WaitForSeconds(keyDelay * Random.Range(0.8f, 1.2f));
			if (c == '\n' || c == '.')
			{
				yield return new WaitForSeconds(keyDelay * Random.Range(0.8f, 1.2f));
			}
			if (skipTriggered)
			{
				skipTriggered = false;
				break;
			}

		}
		txt.text = story;

		startTime = Time.time;
		float endTime = startTime + line.durationAfterTyping;

		line.eventAfterSpeaking.Invoke();
		while (Time.time < endTime)
		{
			yield return new WaitForSeconds(0.1f);
			if (skipTriggered)
			{
				skipTriggered = false;
				break;

			}
		}

	}
}