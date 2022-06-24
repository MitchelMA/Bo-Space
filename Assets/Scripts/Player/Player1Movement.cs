using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1Movement : MonoBehaviour
{
    public float movespeed;
    public float jumpForce;
    public float distToGround = 2.35f;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode attack;
    public KeyCode block;
    public LayerMask groundLayer;

    public LayerMask attackMask;


    private float currentInvis = 0f;
    private float currentHealth;
    private float currentAttackTimeout = 0f;
    private float currentBlockDuration = 0f;
    private float currentBlockTimeout = 0f;


    private Rigidbody2D Rigid1;
    private Transform hitCollider;
    private HitCollider _hitColliderScript;
    private Transform playerChar;
    private CharData playerCharData;
    private Animator spriteAnimator;

    private bool gameOver = false;
    
    private static readonly int XVelocityProperty = Animator.StringToHash("x-velocity");
    private static readonly int YVelocityProperty = Animator.StringToHash("y-velocity");
    private static readonly int AttackTrigger = Animator.StringToHash("AttackTrigger");
    private static readonly int JumpTrigger = Animator.StringToHash("JumpTrigger");
    private static readonly int GroundedProperty = Animator.StringToHash("Grounded");
    private static readonly int BlockingProperty = Animator.StringToHash("Blocking");
    private static readonly int HitTrigger = Animator.StringToHash("HitTrigger");

    public float CurrentHealth
    {
        get => currentHealth;
    }

    public bool GameOver => gameOver;

    /// <summary>
    /// Public boolean that determines if the player is blocking or not
    /// </summary>
    public bool Blocking => spriteAnimator.GetBool(BlockingProperty);
    
    // Start is called before the first frame update
    void Start()
    {
        Rigid1 = GetComponent<Rigidbody2D>();
        hitCollider = transform.GetChild(0).GetChild(1);
        // set the layermask of the filter of the hitCollider
        hitCollider.GetComponent<HitCollider>().AttackMask = attackMask;
        _hitColliderScript = hitCollider.GetComponent<HitCollider>();
        playerChar = transform.GetChild(0);
        playerCharData = playerChar.GetComponent<CharData>();
        // get the animator of the sprite of the character
        spriteAnimator = playerChar.GetChild(0).GetComponent<Animator>();
        Debug.Log(spriteAnimator);

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
            // playerCharData.SetStandSprite();
            spriteAnimator.SetBool(AttackTrigger, false);
            
        }

        if (currentBlockDuration > 0)
        {
            currentBlockDuration -= (1 / 50f);
            spriteAnimator.SetBool(BlockingProperty, true);
        }
        else
        {
            spriteAnimator.SetBool(BlockingProperty, false);
        }

        if (currentBlockTimeout > 0)
        {
            currentBlockTimeout -= (1 / 50f);
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        // set the x-velocity
        spriteAnimator.SetFloat(XVelocityProperty, 0);
        // set the y-velocity
        spriteAnimator.SetFloat(YVelocityProperty, Rigid1.velocity.y);
        // set the grounded property
        spriteAnimator.SetBool(GroundedProperty, Grounded());
        // take no input when game-over
        if (gameOver)
            return;
        if (Blocking == false && currentBlockTimeout <= 0)
        {
            HorVerMove();
        }

        if (Input.GetKeyDown(attack) && currentAttackTimeout <= 0 && spriteAnimator.GetBool(BlockingProperty) == false)
        {
            ToHit();
            currentAttackTimeout = playerCharData.AttackTimeout;
            spriteAnimator.SetBool(AttackTrigger, true);
            // call the attack audio-src
            playerCharData.PunchAudioSrc.Play();
        }

        if (Input.GetKeyDown(block) && currentBlockTimeout <= 0)
        {
            currentBlockTimeout = playerCharData.BlockTimeout;
            currentBlockDuration = playerCharData.BlockDuration;
            spriteAnimator.SetBool(BlockingProperty, true);
        }
        
    }

    private void HorVerMove()
    {
        if (Input.GetKey(left))
        {
            Move(new Vector2(-movespeed, 0));
            transform.localScale = new Vector3(1, 1, 1);
            spriteAnimator.SetFloat(XVelocityProperty, 1);
        }

        if (Input.GetKey(right))
        {
            Move(new Vector2(movespeed, 0));
            transform.localScale = new Vector3(-1, 1, 1);
            spriteAnimator.SetFloat(XVelocityProperty, 1);
        }
        if (Input.GetKeyDown(jump) && Grounded() && _hitColliderScript.CurrentAttackDur <= 0f)
        {
            Rigid1.velocity = new Vector2(Rigid1.velocity.x, jumpForce);
            spriteAnimator.SetTrigger(JumpTrigger);
            // play the jump audio-src
            playerCharData.JumpAudioSrc.Play();
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

        RaycastHit2D hit = Physics2D.Raycast(pos, dir, distToGround, groundLayer);
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
            // activate the `HitTrigger` for the animation to start
            spriteAnimator.SetTrigger(HitTrigger);
            currentHealth -= damage;
            currentInvis = playerCharData.MaxInvis;
            if (currentHealth <= 0)
            {
                Debug.Log("Dead");
                gameOver = true;
            }
        }
    }

    public void ResetBlock()
    {
        currentBlockTimeout = 0;
    }
}
