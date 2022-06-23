using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GetVolume : MonoBehaviour
{
    [SerializeField] private string settingsObjectTag;
    [SerializeField] private AudioSource audioSource;

    private GameObject _settingsObject;
    private Settings _settingsData;
    // Start is called before the first frame update
    void Start()
    {
        _settingsObject = GameObject.FindWithTag(settingsObjectTag);
        _settingsData = _settingsObject.GetComponent<Settings>();
    }

    // Update is called once per frame
    void Update()
    {
        // convert the volume from % to a 0 - 1 range
        audioSource.volume = _settingsData.Volume / 100;
    }
}
