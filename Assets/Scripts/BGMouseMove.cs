using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMouseMove : MonoBehaviour
{
    [SerializeField] private float multiplier = 1f;
    [SerializeField] private GameObject background;
    private RectTransform _bgTransform;
    private Camera _camera;

    private Vector2 _mousePos;
    // Start is called before the first frame update
    private void Start()
    {
        // get the rect transform
        _bgTransform = background.GetComponent<RectTransform>();
        _camera = Camera.main!;
    }

    // Update is called once per frame
    private void Update()
    {
        BackgroundUpdate();
    }

    /// <summary>
    /// Updates the background such that it moves with the position of the mouse
    /// </summary>
    private void BackgroundUpdate()
    {
        // get the position of the mouse
        Vector3 mousePos = Input.mousePosition;
        // set the z-index to be 10 units away from the camera, else it might not work
        mousePos.z = 10;
        // get the 3d position
        Vector3 mouse3D = _camera.ScreenToWorldPoint(mousePos);
        // convert it to a 2d position
        _mousePos = mouse3D;
        
        // sets the size of the background to always be that of the screen of the player
        _bgTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        
        // and now set the position of the background
        _bgTransform.anchoredPosition = new Vector3(
            _mousePos.x * (multiplier - 1), 
            _mousePos.y * (multiplier - 1), 
            0);
    }
}
