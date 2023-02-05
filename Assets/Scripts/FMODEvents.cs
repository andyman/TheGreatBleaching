using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [SerializeField]
    public EventReference music;

    [Header("Player SFX")]
    [SerializeField]
    public EventReference playerFootsteps;

    [SerializeField]
    public EventReference playerLanding;

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene");
        }
        instance = this;
    }

}
