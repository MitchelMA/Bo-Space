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
    private float _referenceFOVSize = 0;
    private float _fovSize = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _referenceFOVSize = Mathf.Abs(referenceCameraDistance) * Mathf.Tan(camera.fieldOfView * Mathf.PI / 180);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerOnePos = playerOne.position;
        Vector3 playerTwoPos = playerTwo.position;
        Vector3 newPos = Vector3.Lerp(playerOnePos, playerTwoPos, 0.5f);
        newPos.z = Tools.Clamp(CalcZ(), minCameraValues.z, maxCameraValues.z);
        _fovSize = Mathf.Abs(newPos.z) * Mathf.Tan(camera.fieldOfView * Mathf.PI / 180);
        float fovDiff = _fovSize - _referenceFOVSize;
        newPos.y = Tools.Clamp(newPos.y, minCameraValues.y, maxCameraValues.y);
        newPos.x = Tools.Clamp(newPos.x, minCameraValues.x + fovDiff/2, maxCameraValues.x - fovDiff/2);
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
