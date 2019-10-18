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

    // Update is called once per frame
    void Update()
    {
        Vector3 TempVect = PlayerTransform.position + PositionOffset;
        //TempVect.y = 3.22135f + PositionOffset.y;
        transform.position = TempVect;
    }
}
