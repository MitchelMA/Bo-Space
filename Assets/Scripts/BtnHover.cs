using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class BtnHover : MonoBehaviour
{
    private Image _image;
    private Button _button;

    private Sprite _standSprite;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _standSprite = _image.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HoverEnter(Sprite enterSprite)
    {
        _image.sprite = enterSprite;
    }

    public void HoverExit()
    {
        _image.sprite = _standSprite;
    }
}
