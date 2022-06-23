using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupData : MonoBehaviour
{
    [SerializeField] private string playerOneTag;
    [SerializeField] private string playerTwoTag;
    
    [SerializeField] private string dataTag;

    private GameObject _playerOneParent;
    private GameObject _playerTwoParent;

    private GameObject _dataObject;
    private OptionData _optionData;
    // Start is called before the first frame update

    private void Awake()
    {
        Debug.Log("Awakened");
        _playerOneParent = GameObject.FindWithTag(playerOneTag);
        _playerTwoParent = GameObject.FindWithTag(playerTwoTag);

        _dataObject = GameObject.FindWithTag(dataTag);
        _optionData = _dataObject.GetComponent<OptionData>();
        
        GameObject charOne = Instantiate(_optionData.PlayerOneChar.Character, _playerOneParent.transform);
        GameObject charTwo = Instantiate(_optionData.PlayerTwoChar.Character, _playerTwoParent.transform);
        
        // now delete the data object
        Destroy(_dataObject);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
