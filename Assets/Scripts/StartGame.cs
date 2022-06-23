using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private OptionData _optionData;
    private GameObject _dataObject;
    [SerializeField] private Button button;
    
    // Start is called before the first frame update
    void Start()
    {
        _dataObject = GameObject.FindWithTag("DataHolder");
        _optionData = _dataObject.GetComponent<OptionData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        // check if all character-options aren't empty
        if (_optionData.PlayerOneChar.Empty || _optionData.PlayerTwoChar.Empty)
        {
            // do nothing in this case, since not all players have chosen a character
            return;
        }
        // check if a scene is selected
        if (_optionData.StageName == null)
        {
            return;
        }
        // get the name of the stage in the option-data and load that scene in
        SceneManager.LoadScene(_optionData.StageName, LoadSceneMode.Single);
    }
}
