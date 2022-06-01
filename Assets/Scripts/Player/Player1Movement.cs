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
    public float attackDuration = 0.1f;
    // the attacktimeout: how many seconds it takes before you can attack again
    public float attackTimeout = 0.8f;
    public float hitDamage = 10f;
    public float maxInvis = 1f;
    public float maxHealth = 100f;

    private float currentInvis = 0f;
    public float currentHealth;
    private float currentAttackTimeout = 0f;

    private Rigidbody2D Rigid1;


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
    }

    // Start is called before the first frame update
    void Start()
    {
        Rigid1 = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
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
            currentAttackTimeout = attackTimeout;
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
        _ = Instantiate(hitPref, transform);
    }

    public void TakeDamage(float damage)
    {
        if (currentInvis <= 0f)
        {
            currentHealth -= damage;
            currentInvis = maxInvis;
            if (currentHealth <= 0)
            {
                Debug.Log("Dead");
                Debug.Log($"{gameObject.name} Lost!");
                SceneManager.LoadScene("MenuScreen", LoadSceneMode.Single);
            }
        }
    }
}
