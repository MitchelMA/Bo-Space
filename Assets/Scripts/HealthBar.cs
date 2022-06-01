using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Slider slider;

    private Player1Movement playerMov;

    private CharData playerData;

    private Slider.Direction dir;
    // Start is called before the first frame update
    void Start()
    {
        playerMov = player.GetComponent<Player1Movement>();
        playerData = playerMov.transform.GetChild(0).GetComponent<CharData>();
        dir = slider.direction;
    }
    
    // Update is called once per frame
    void Update()
    {
        float per = playerMov.CurrentHealth / playerData.MaxHealth;
        slider.value = per;
    }
}
