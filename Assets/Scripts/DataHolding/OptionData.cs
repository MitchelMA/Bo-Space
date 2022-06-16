using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionData : MonoBehaviour
{
    // character selection
    private CharOption _playerOneChar = CharOption.EmptyCon();
    private CharOption _playerTwoChar = CharOption.EmptyCon();

    private string _stageName;
    
    public CharOption PlayerOneChar
    {
        get => _playerOneChar;
        set => _playerOneChar = value;
    }
    public CharOption PlayerTwoChar
    {
        get => _playerTwoChar;
        set => _playerTwoChar = value;
    }

    public string StageName
    {
        get => _stageName;
        set => _stageName = value;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
