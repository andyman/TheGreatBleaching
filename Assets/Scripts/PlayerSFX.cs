using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class PlayerSFX : MonoBehaviour
{
    private EventInstance playerFootsteps;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        playerFootsteps = RuntimeManager.CreateInstance(FMODEvents.instance.playerFootsteps);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateSound();
    }

    private void UpdateSound()
    {
        if(transform.GetComponent<FlatSpeedCalculator>().flatSpeed > 0)
        {
            RuntimeManager.PlayOneShot(FMODEvents.instance.playerFootsteps, transform.position);
        }

    }
}
