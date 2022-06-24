using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    [SerializeField] private string settingsObjectTag;
    [SerializeField] private Slider slider;
    [SerializeField] private Settings.AudioTypes audioType;

    private GameObject _settingsObject;
    private Settings _settingsData;
    // Start is called before the first frame update
    void Start()
    {
        _settingsObject = GameObject.FindWithTag(settingsObjectTag);
        _settingsData = _settingsObject.GetComponent<Settings>();
        // get the values of the settingsData object
        slider.value = _settingsData.Volume[audioType] / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVolume()
    {
        // save the volume in %
        _settingsData.Volume[audioType] = slider.value * 100;
    }   
}
