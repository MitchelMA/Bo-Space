using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionData : MonoBehaviour
{
    // character selection
    // Character option of player one
    private CharOption _playerOneChar = CharOption.EmptyCon();
    // character option of player two
    private CharOption _playerTwoChar = CharOption.EmptyCon();

    // stage selection
    // name of the chosen stage
    private string _stageName;
    
    /// <summary>
    /// Character option of player one
    /// This contains a prefab and the corresponding name of the chosen character
    /// </summary>
    public CharOption PlayerOneChar
    {
        get => _playerOneChar;
        set => _playerOneChar = value;
    }
    /// <summary>
    /// Character option of player two
    /// This contains a prefab and the corresponding name of the chosen character
    /// </summary>
    public CharOption PlayerTwoChar
    {
        get => _playerTwoChar;
        set => _playerTwoChar = value;
    }

    /// <summary>
    /// Name of the chosen stage
    /// </summary>
    public string StageName
    {
        get => _stageName;
        set => _stageName = value;
    }

    /// <summary>
    /// Public method to clear the chosen option of a player
    /// </summary>
    /// <param name="playerIndex">The index of the player whose option that gets cleared</param>
    public void ClearChar(int playerIndex)
    {
        switch (playerIndex)
        {
            case 0:
                _playerOneChar.SetEmpty();
                break;
            case 1:
                _playerTwoChar.SetEmpty();
                break;
            default:
                break;
        }
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
