using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    
    // players for which the gameOver should be checked
    [SerializeField] private GameObject playerOne;
    [SerializeField] private GameObject playerTwo;
    [SerializeField] private float gameOverWait = 0.8f;

    private float _currentWait;

    private Player1Movement _movementOne;
    private Player1Movement _movementTwo;

    private bool _playerOneOver;
    private bool _playerTwoOver;
    private bool _done = false;

    public bool PlayerOneOver => _playerOneOver;
    public bool PlayerTwoOver => _playerTwoOver;

    // Start is called before the first frame update
    void Start()
    {
        _currentWait = gameOverWait;
        
        _movementOne = playerOne.GetComponent<Player1Movement>();
        _movementTwo = playerTwo.GetComponent<Player1Movement>();
    }

    private void FixedUpdate()
    {
        // when the game-over is "done", the script shouldn't try to deplete the game over
        // this so it won't continuously try to load in the next scene
        if (_done == false && (_playerOneOver || _playerTwoOver))
        {
            GameOverDeplete();
        }
    }

    private void GameOverDeplete()
    {
        _currentWait -= (1 / 50f);
        
        // game-over
        if (_currentWait <= 0)
        {
            _done = true;
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check for player game-overs
        _playerOneOver = _movementOne.GameOver;
        _playerTwoOver = _movementTwo.GameOver;
    }
}
