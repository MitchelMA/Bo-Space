using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCharOption : MonoBehaviour
{
    // Start is called before the first frame update
    private OptionData _data;
    void Start()
    {
        _data = GameObject.FindWithTag("DataHolder").GetComponent<OptionData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Public method to clear a player's option
    /// </summary>
    /// <param name="index">the index of the player whose option gets cleared</param>
    public void ClearOption(int index)
    {
        _data.ClearChar(index);
    }
}
