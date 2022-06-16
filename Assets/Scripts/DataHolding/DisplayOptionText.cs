using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayOptionText : MonoBehaviour
{
    private OptionData _data;
    private Text _text;

    [SerializeField] private int playerIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        _data = GameObject.FindWithTag("DataHolder").GetComponent<OptionData>();
    }

    private void FixedUpdate()
    {
        switch (playerIndex)
        {
            case 0:
                // early break when the option is empty
                if (_data.PlayerOneChar.Empty)
                {
                    _text.text = "";
                    break;
                }
                
                _text.text = _data.PlayerOneChar.Name;
                break;
            case 1:
                // early break when the option is empty
                if (_data.PlayerTwoChar.Empty)
                {
                    _text.text = "";
                    break;
                }
                
                _text.text = _data.PlayerTwoChar.Name;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
