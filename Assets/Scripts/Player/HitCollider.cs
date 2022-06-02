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
        float attackDur = parent.GetComponent<CharData>().AttackDuration;
        float attackDamage = parent.GetComponent<CharData>().HitDamage;
        var overlapList = new List<Collider2D>();
        if (collider.OverlapCollider(filter, overlapList) > 0)
        {
            foreach (var col in overlapList)
            {
                var rigid = col.GetComponent<Rigidbody2D>();
                Debug.Log(col.name);
                col.GetComponent<Player1Movement>().TakeDamage(attackDamage);
                // calculate the vector with its base the position of this player and
                // its head pointing towards the opponent, and point it a little upwards
                Vector3 force = ((col.transform.position - transform.parent.transform.position) * 500 + Vector3.up).normalized;
                // add the force directly to the velocity
                rigid.velocity = force * forceMultiplier;
                // destroy the collider after the duration of the attack is over
                Destroy(gameObject, attackDur);
            }
        }
        Destroy(gameObject, attackDur);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
