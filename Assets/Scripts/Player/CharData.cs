using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CharData : MonoBehaviour
{
    [SerializeField]
    private Sprite standSprite;
    private Transform child;
    private SpriteRenderer childSprite;
    [SerializeField]
    private Vector3 standScale;
    [SerializeField]
    private GameObject hitPref;
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
    /// <summary>
    /// Public property to get the max-health of the CharData script
    /// </summary>
    public float MaxHealth { get => maxHealth; }
    /// <summary>
    /// Public property to get the duration of the attack of the CharData script
    /// </summary>
    public float AttackDuration { get => attackDuration; }
    /// <summary>
    /// Public property to get the timeout until which an attack can be performed again
    /// </summary>
    public float AttackTimeout { get => attackTimeout; }
    /// <summary>
    /// Public property to get the damage a hit will performed
    /// </summary>
    public float HitDamage { get => hitDamage; }
    /// <summary>
    /// Public property to get the duration of invisibility frames
    /// </summary>
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

    /// <summary>
    /// Instantiates the specified prefab to be used for hit-collision detection
    /// </summary>
    public void PrefabCollider()
    {
        // get the control-script of the hit-prefab
        var ctrlScript = hitPref.GetComponent<HitCollider>();
        // now set the collider as active
        ctrlScript.SetActive();
    }
    /// <summary>
    /// Revert the current-displaying sprite back to the standard sprite.
    /// The standard sprite is defined in the Start() method, and is the sprite
    /// the sprite renderer had at the start of the scene
    /// </summary>
    public void SetStandSprite()
    {
        childSprite.sprite = standSprite;
        child.localScale = standScale;
        transform.localPosition = new Vector3(0, 0, 0);
    }
}
