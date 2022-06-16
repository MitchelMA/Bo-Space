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

    public void ClearOption(int index)
    {
        switch (index)
        {
            case 0:
            {
                _data.PlayerOneChar.SetEmpty();
            }
                break;
            case 1:
            {
                _data.PlayerTwoChar.SetEmpty();
            }
                break;
            default:
                break;
        }
    }
}
