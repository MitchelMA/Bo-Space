using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public void Click(GameObject audioObject)
    {
        // instantiate the audio-prefab so it can play the audio
        Instantiate(audioObject);
    }
}
