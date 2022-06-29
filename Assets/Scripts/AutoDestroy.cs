using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void FixedUpdate()
    {
        // auto-destroy this object when the audio-source is done playing
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
