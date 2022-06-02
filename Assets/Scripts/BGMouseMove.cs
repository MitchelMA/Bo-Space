using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMouseMove : MonoBehaviour
{
    [SerializeField] private float multiplier = 1f;
    [SerializeField] private GameObject background;
    private RectTransform _bgTransform;

    private Vector2 _mousePos;
    // Start is called before the first frame update
    void Start()
    {
        // get the rect transform
        _bgTransform = background.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundUpdate();
    }

    /// <summary>
    /// Updates the background such that it moves with the position of the mouse
    /// </summary>
    private void BackgroundUpdate()
    {
        Camera main = Camera.main!;
        Vector3 mouse3D = main.ScreenToWorldPoint(Input.mousePosition);
        _mousePos = new Vector2(mouse3D.x, mouse3D.y);
        Vector2 unit = _mousePos.normalized;
        float dist = _mousePos.magnitude;

        _bgTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        
        _bgTransform.anchoredPosition = new Vector3(
            (dist * unit.x) * (multiplier - 1), 
            (dist * unit.y) * (multiplier - 1), 
            0);
    }
}
