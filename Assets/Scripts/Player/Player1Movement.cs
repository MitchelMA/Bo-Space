using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    public float movespeed;
    public float jumpForce;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    private Rigidbody2D Rigid1;
    public LayerMask groundLayer;




    // Start is called before the first frame update
    void Start()
    {
        Rigid1 = GetComponent<Rigidbody2D>();
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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(left))
        {
            Rigid1.velocity = new Vector2(-movespeed, Rigid1.velocity.y);
        }
        else if(Input.GetKey(right))
        {
            Rigid1.velocity = new Vector2(movespeed, Rigid1.velocity.y);
        }
        else
        {
            Rigid1.velocity = new Vector2(0, Rigid1.velocity.y);
        }

        if(Input.GetKeyDown(jump) && Grounded())
        {
            Rigid1.velocity = new Vector2(Rigid1.velocity.x, jumpForce);
        }
    }


}
