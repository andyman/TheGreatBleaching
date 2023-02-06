using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private EventInstance musicEventInstance;
    private EventInstance ambienceEventInstance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene");
        }
        instance = this;
    }

    private void StartMusic(EventReference musicEventReference)
    {
        musicEventInstance = RuntimeManager.CreateInstance(musicEventReference);
        musicEventInstance.start();
    }

    private void StartAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = RuntimeManager.CreateInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartMusic(FMODEvents.instance.music);
        StartAmbience(FMODEvents.instance.ambience);
    }

    public void PlayAudio(string eventName)
    {
        RuntimeManager.PlayOneShot(eventName, Camera.main.transform.position);
    }
}
