using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    // the volume in %
    private float _volume = 100;

    /// <summary>
    /// The public property of the settings-data that represents the volume in %
    /// </summary>
    public float Volume
    {
        get => _volume;
        set => _volume = value; 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_volume);
    }
}
