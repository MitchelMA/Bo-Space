using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{

    private BoxCollider2D collider;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private ContactFilter2D filter;
    [SerializeField]
    private float forceMultiplier = 5f;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        Transform parent = gameObject.transform.parent;
        float attackDur = parent.GetComponent<Player1Movement>().attackDuration;
        float attackDamage = parent.GetComponent<Player1Movement>().hitDamage;
        var overlapList = new List<Collider2D>();
        if(collider.OverlapCollider(filter, overlapList) > 0)
        {
            foreach (var col in overlapList)
            {
                Debug.Log(col.name);
                //Debug.Log(col);
                col.GetComponent<Player1Movement>().TakeDamage(attackDamage);
                // calculate the vector with its base the position of this player and
                // its head pointing towards the opponent
                Vector3 force = (col.transform.position - transform.parent.transform.position).normalized;
                //Debug.Log(force);
                col.GetComponent<Rigidbody2D>().AddForce(force * forceMultiplier, ForceMode2D.Force);
                Destroy(gameObject, attackDur);
            }
        } else
        {
            Destroy(gameObject, attackDur);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
