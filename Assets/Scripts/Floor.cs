using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private BoxCollider2D _collider;
    private List<Collider2D> _overlaps;
    [SerializeField]
    private ContactFilter2D filter;
    
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _overlaps = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_collider.OverlapCollider(filter, _overlaps) > 0)
        {
            HandleOverlaps();
        }
    }

    /// <summary>
    /// Kills all overlapping players who are specified by the filter
    /// </summary>
    private void HandleOverlaps()
    {
        foreach(Collider2D overlap in _overlaps)
        {
            Player1Movement mov;
            CharData data;
            if(!overlap.transform.GetChild(0).TryGetComponent<CharData>(out data)) { return; }
            if(overlap.TryGetComponent<Player1Movement>(out mov))
            {
                mov.TakeDamage(data.MaxHealth);
            }
        }
    }
}
