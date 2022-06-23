using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupData : MonoBehaviour
{
    [SerializeField] private string playerOneTag;
    [SerializeField] private string playerTwoTag;
    
    [SerializeField] private string dataTag;

    [SerializeField] private Text playerOneName;
    [SerializeField] private Text playerTwoName;

    private GameObject _playerOneParent;
    private GameObject _playerTwoParent;

    private GameObject _dataObject;
    private OptionData _optionData;

    // Awake is called before start
    private void Awake()
    {
        // find the player-parents by tag
        _playerOneParent = GameObject.FindWithTag(playerOneTag);
        _playerTwoParent = GameObject.FindWithTag(playerTwoTag);
        
        // get the data with the tag
        _dataObject = GameObject.FindWithTag(dataTag);
        _optionData = _dataObject.GetComponent<OptionData>();
        
        // instantiate the player prefabs
        Instantiate(_optionData.PlayerOneChar.Character, _playerOneParent.transform);
        Instantiate(_optionData.PlayerTwoChar.Character, _playerTwoParent.transform);

        // now set the names above their respective health-bars
        playerOneName.text = _optionData.PlayerOneChar.Name;
        playerTwoName.text = _optionData.PlayerTwoChar.Name;
        
        // now delete the data object
        Destroy(_dataObject);
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
