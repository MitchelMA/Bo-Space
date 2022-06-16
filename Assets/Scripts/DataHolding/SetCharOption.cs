using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharOption : MonoBehaviour
{
    private OptionData _data;

    [SerializeField] private GameObject charPrefab;

    [SerializeField] private string charName;
    // Start is called before the first frame update
    void Start()
    {
        _data = GameObject.FindWithTag("DataHolder").GetComponent<OptionData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// Public method to set the option of the players
    /// if player one's option is empty, player one may choose
    /// if player one's option is not empty and player two's option *is* empty, player two may choose
    /// else nothing will happen
    /// </summary>
    public void SetOption()
    {
        // only set player one when player one is not empty
        if (_data.PlayerOneChar.Empty)
        {
            Debug.Log("Set player one");
            CharOption option = new CharOption(charPrefab, charName);
            _data.PlayerOneChar = option;
            return;
        }
        
        // and only set the player two when player one is empty and player two is not
        if (_data.PlayerTwoChar.Empty)
        {
            Debug.Log("Set player two");
            CharOption option = new CharOption(charPrefab, charName);
            _data.PlayerTwoChar = option;
        }
    }
}
