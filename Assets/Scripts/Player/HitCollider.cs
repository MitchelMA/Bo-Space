using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{

    private BoxCollider2D collider;
    private ContactFilter2D filter;
    [SerializeField]
    private float forceMultiplier = 5f;

    private bool isActive = false;

    private float maxAttackDur;
    private float attackDamage;

    private float currentAttackDur = 0;
    private List<Collider2D> overlapList = new List<Collider2D>();

    /// <summary>
    /// Public method to set the collider as active.
    /// </summary>
    public void SetActive()
    {
        isActive = true;
        // also set the currentAttackDuration to the maxAttackDuration
        // by doing this, we can keep track when the collider should be set back to inactive
        currentAttackDur = maxAttackDur;
        // set the filter to use the LayerMask, this is a simple but useful way to reduce noise in collision-detection
        filter.useLayerMask = true;
    }
    public LayerMask AttackMask
    {
        set => filter.layerMask = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        Transform parent = gameObject.transform.parent;
        maxAttackDur = parent.GetComponent<CharData>().AttackDuration;
        attackDamage = parent.GetComponent<CharData>().HitDamage;
    }

    private void FixedUpdate()
    {
        // do nothing if the collider is set to inactive
        if (!isActive)
        {
            return;
        }
        
        if (collider.OverlapCollider(filter, overlapList) > 0)
        {
            HandleOverlaps();
        }

        // system to set the collider as inactive after a specified duration
        if (currentAttackDur >= 0)
        {
            currentAttackDur -= 1 / 50f;
        }

        if (currentAttackDur <= 0)
        {
            isActive = false;
        }
    }

    /// <summary>
    /// Method that handles overlaps of the collider.
    /// It will apply a knock-back to the opposing player
    /// </summary>
    private void HandleOverlaps()
    {
        foreach (var col in overlapList)
        {
            var rigid = col.GetComponent<Rigidbody2D>();
            Debug.Log(col.name);
            col.GetComponent<Player1Movement>().TakeDamage(attackDamage);
            // calculate the vector with its base the position of this player and
            // its head pointing towards the opponent, and point it a little upwards
            Vector3 force = ((col.transform.position - transform.parent.transform.parent.position) * 500 + Vector3.up).normalized;
            // add the force directly to the velocity
            rigid.velocity = force * forceMultiplier;
            // destroy the collider after the duration of the attack is over
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
