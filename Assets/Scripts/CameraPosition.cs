using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{

    public Transform PlayerTransform;
    public Vector3 PositionOffset;

    private void Start()
    {
        
    }

    void Update()
    {

        Vector3 TempVect = PlayerTransform.position + PositionOffset;
        transform.position = TempVect;

    }
}
