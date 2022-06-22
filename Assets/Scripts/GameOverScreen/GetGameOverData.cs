using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetGameOverData : MonoBehaviour
{
    [SerializeField] private string gameOverTag;
    [SerializeField] private Text gameOverText;
    private GameObject _gameOverObject;
    private GameOver _gameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameOverObject = GameObject.FindWithTag(gameOverTag);
        _gameOver = _gameOverObject.GetComponent<GameOver>();

        if (_gameOver.PlayerOneOver && _gameOver.PlayerTwoOver)
        {
            gameOverText.text = "It Was A Tie!";
        }
        else
        {
            gameOverText.text = _gameOver.PlayerOneOver ? "Player Two Won!" : "PLayer One Won!";
        }
        
        // after all this, the game-over object can be destroyed
        Destroy(_gameOverObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
