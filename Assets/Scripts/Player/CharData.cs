using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharData : MonoBehaviour
{
    [SerializeField]
    private Sprite attackSprite;
    private Sprite standSprite;
    private Transform child;
    private SpriteRenderer childSprite;
    [SerializeField]
    private float attackScale;
    private Vector3 standScale;
    private Object hitPref;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float attackDuration = 0.1f;
    [SerializeField]
    private float attackTimeout = 0.8f;
    [SerializeField]
    private float hitDamage = 10f;
    [SerializeField]
    private float maxInvis = 1f;

    // public properties
    public float MaxHealth { get => maxHealth; }
    public float AttackDuration { get => attackDuration; }
    public float AttackTimeout { get => attackTimeout; }
    public float HitDamage { get => hitDamage; }
    public float MaxInvis { get => maxInvis; }
    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0);
        childSprite = child.GetComponent<SpriteRenderer>();
        standSprite = childSprite.sprite;
        standScale = child.localScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PrefabCollider()
    {
        _ = Instantiate(hitPref, transform);
        childSprite.sprite = attackSprite;
        child.localScale = new Vector3(attackScale, attackScale, 1);
    }

    public void SetStandSprite()
    {
        childSprite.sprite = standSprite;
        child.localScale = standScale;
    }

    public void SetHitPrefab(Object prefab)
    {
        this.hitPref = prefab;
    }
}
