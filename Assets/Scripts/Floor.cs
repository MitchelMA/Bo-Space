using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private BoxCollider2D collider;
    private List<Collider2D> overlaps;
    [SerializeField]
    private ContactFilter2D filter;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        overlaps = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(collider.OverlapCollider(filter, overlaps) > 0)
        {
            foreach(Collider2D overlap in overlaps)
            {
                Player1Movement mov;
                if(overlap.TryGetComponent<Player1Movement>(out mov))
                {
                    mov.TakeDamage(mov.maxHealth);
                }
            }
        }
    }
}
