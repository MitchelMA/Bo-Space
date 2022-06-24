using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public enum AudioTypes
    {
        Masters,
        Background,
        SFX,
    }
    // the volume in %
    private readonly Dictionary<AudioTypes, float> _volume = new Dictionary<AudioTypes, float>()
    {
        {AudioTypes.Masters, 100},
        {AudioTypes.Background, 100},
        {AudioTypes.SFX, 100}
    };

    /// <summary>
    /// The public property of the settings-data that represents the volume in %
    /// </summary>
    public Dictionary<AudioTypes, float> Volume => _volume;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"Audio:\nMasters: {_volume[AudioTypes.Masters]}\tBackground: {_volume[AudioTypes.Background] / 100 * _volume[AudioTypes.Masters]}\tSFX: {_volume[AudioTypes.SFX] / 100 * _volume[AudioTypes.Masters]}");
    }
}
