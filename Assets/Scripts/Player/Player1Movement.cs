using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1Movement : MonoBehaviour
{
    public float movespeed;
    public float jumpForce;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode attack;
    public LayerMask groundLayer;

    public LayerMask attackMask;

    private float currentInvis = 0f;
    private float currentHealth;
    private float currentAttackTimeout = 0f;


    private Rigidbody2D Rigid1;
    private Transform hitCollider;
    private Transform playerChar;
    private CharData playerCharData;

    private float gameOverTimeout = 0.7f;
    private bool gameOver = false;
    
    public float CurrentHealth
    {
        get => currentHealth;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Rigid1 = GetComponent<Rigidbody2D>();
        hitCollider = transform.GetChild(0).GetChild(1);
        // set the layermask of the filter of the hitCollider
        hitCollider.GetComponent<HitCollider>().AttackMask = attackMask;
        playerChar = transform.GetChild(0);
        playerCharData = playerChar.GetComponent<CharData>();
        // set the hit prefab of the chardata
        // playerCharData.HitPrefab = hitPref;

        //set the current health
        currentHealth = playerCharData.MaxHealth;
    }
    
    private void FixedUpdate()
    {
        if (currentInvis > 0)
        {
            currentInvis -= (1 / 50f);
        }
        if (currentAttackTimeout > 0)
        {
            currentAttackTimeout -= (1 / 50f);
        }
        if(currentAttackTimeout <= 0)
        {
            playerCharData.SetStandSprite();
        }

        if (gameOver)
        {
            GameOverDeplete();
        }
    }

    /// <summary>
    /// Depletes the time of the gameOverTimeout variable.
    /// When this timeout hits 0 or lower, the game will switch to the menu-screen
    /// </summary>
    private void GameOverDeplete()
    {
        gameOverTimeout -= (1 / 50f);
        if (gameOverTimeout <= 0)
        {
            SceneManager.LoadScene("MenuScreen", LoadSceneMode.Single);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // take no input when game-over
        if (gameOver)
            return;
        if (Input.GetKey(left))
        {
            // Rigid1.velocity = new Vector2(-movespeed, Rigid1.velocity.y);
            Move(new Vector2(-movespeed, Rigid1.velocity.y));
        }
        else if (Input.GetKey(right))
        {
            Move(new Vector2(movespeed, Rigid1.velocity.y));
        }
        // rotate the character towards his movement
        if (Input.GetKeyDown(left))
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetKeyDown(right))
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKeyDown(attack) && currentAttackTimeout <= 0)
        {
            ToHit();
            currentAttackTimeout = playerCharData.AttackTimeout;
        }

        if (Input.GetKeyDown(jump) && Grounded())
        {
            Rigid1.velocity = new Vector2(Rigid1.velocity.x, jumpForce);
            // Move(new Vector2(Rigid1.velocity.x, jumpForce));
        }
    }
    /// <summary>
    /// Uses a Physics2D raycast to check if the player is grounded.
    /// It reduces the noise of colliding with all objects by making use of a LayerMask
    /// </summary>
    /// <returns> A boolean which determines if the player is grounded or not</returns>
    private bool Grounded()
    {
        Vector2 pos = transform.position;
        Vector2 dir = Vector2.down;
        float dist = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(pos, dir, dist, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    private void Move(Vector2 moveVector)
    {
        transform.position += new Vector3(moveVector.x, moveVector.y) * Time.deltaTime;
    }

    private void ToHit()
    {
        playerCharData.PrefabCollider();
    }

    /// <summary>
    /// Method to make the player take damage
    /// It makes this instance take the specified amount of damage
    /// </summary>
    /// <param name="damage">The damage you want to make the instance take</param>
    public void TakeDamage(float damage)
    {
        if (currentInvis <= 0f)
        {
            currentHealth -= damage;
            currentInvis = playerCharData.MaxInvis;
            if (currentHealth <= 0)
            {
                Debug.Log("Dead");
                Debug.Log($"{gameObject.name} Lost!");
                gameOver = true;
            }
        }
    }
}
