using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FollowPlayers : MonoBehaviour
{
    [SerializeField] 
    private Transform playerOne;
    [SerializeField]
    private Transform playerTwo;
    
    // Camera peremiter
    [SerializeField] private Vector3 minCameraValues = new Vector3(-10, 0.3f, -9);
    [SerializeField] private Vector3 maxCameraValues = new Vector3(10, 10, -3f);

    [SerializeField] private float slope = -3;
    [SerializeField] private Vector2 referencePoint = new Vector2(6, -9);

    [SerializeField] private Camera camera;
    [SerializeField] private float referenceCameraDistance = -9;
    private float _referenceHFOVSize = 0;
    private float _referenceVFOVSize = 0;
    private float _HFovSize = 0;
    private float _VFovSize = 0;

    private float HorizonalFov
    {
        get 
        {
            float radAngle = camera.fieldOfView * Mathf.Deg2Rad;
            float radHFov = 2 * Mathf.Atan(Mathf.Tan(radAngle / 2) * camera.aspect);
            return Mathf.Rad2Deg * radHFov;
        }
    }

    private float VerticalFov => camera.fieldOfView;

    // Start is called before the first frame update
    void Start()
    {
        // calculate the reference FOV sizes of the horizontal and vertical axixes
        _referenceHFOVSize = Mathf.Abs(referenceCameraDistance) * Mathf.Tan(HorizonalFov * Mathf.Rad2Deg);
        _referenceVFOVSize = Mathf.Abs(referenceCameraDistance) * Mathf.Tan(VerticalFov * Mathf.Rad2Deg);
    }

    // Update is called once per frame
    void Update()
    {
        // get the positions of the players
        Vector3 playerOnePos = playerOne.position;
        Vector3 playerTwoPos = playerTwo.position;
        
        // linear interpolate at half the distance of the players
        Vector3 newPos = Vector3.Lerp(playerOnePos, playerTwoPos, 0.5f);
        
        // calculate the new Z position of the camera with the formula z = ax + b;
        newPos.z = Tools.Clamp(CalcZ(), minCameraValues.z, maxCameraValues.z);
        
        // get the current FOV sizes
        _HFovSize = Mathf.Abs(newPos.z) * Mathf.Tan(HorizonalFov * Mathf.Rad2Deg);
        _VFovSize = Mathf.Abs(newPos.z) * Mathf.Tan(VerticalFov * Mathf.Rad2Deg);
        
        // calculate the difference
        float HFovDiff = _HFovSize - _referenceHFOVSize;
        float VFovDiff = _VFovSize - _referenceVFOVSize;
        
        // adjust the clamping values according to the FOV sizes
        // DON'T ADJUST THE MAX VALUE OF THE Y-POS!
        // When you do this, the max could get less than the min
        newPos.y = Tools.Clamp(newPos.y, minCameraValues.y + VFovDiff, maxCameraValues.y);
        newPos.x = Tools.Clamp(newPos.x, minCameraValues.x + HFovDiff/4, maxCameraValues.x - HFovDiff/4);
        
        // set the camera at its new position
        transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
    }

    private float CalcZ()
    {
        // calculate the distance between the two players
        float dist = (playerOne.position - playerTwo.position).magnitude;
        // calculate the y-offset `b`
        float b = referencePoint.y - slope * referencePoint.x;
        
        // return the value of the applied formula
        return slope * dist + b;
    }
}
