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

    public Object hitPref;

    private float currentInvis = 0f;
    private float currentHealth;
    private float currentAttackTimeout = 0f;


    private Rigidbody2D Rigid1;
    private Transform playerChar;
    private CharData playerCharData;

    private float gameOverTimeout = 0.7f;
    private bool gameOver = false;
    
    public float CurrentHealth
    {
        get => currentHealth;
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
            gameOverTimeout -= (1 / 50f);
            if (gameOverTimeout <= 0)
            {
                SceneManager.LoadScene("MenuScreen", LoadSceneMode.Single);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Rigid1 = GetComponent<Rigidbody2D>();
        playerChar = transform.GetChild(0);
        playerCharData = playerChar.GetComponent<CharData>();
        // set the hit prefab of the chardata
        playerCharData.SetHitPrefab(hitPref);

        //set the current health
        currentHealth = playerCharData.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // take no input when game-over
        if (gameOver)
            return;
        if (Input.GetKey(left))
        {
            Rigid1.velocity = new Vector2(-movespeed, Rigid1.velocity.y);
        }
        else if (Input.GetKey(right))
        {
            Rigid1.velocity = new Vector2(movespeed, Rigid1.velocity.y);
        }
        else
        {
            Rigid1.velocity = new Vector2(0, Rigid1.velocity.y);
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
        }
    }

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

    private void ToHit()
    {
        playerCharData.PrefabCollider();
    }

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
