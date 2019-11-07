using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnlySomewhere : MonoBehaviour
{
    public float ComeBackForce = 2000;

    void Update()
    {
        if (transform.position.z > -34f)
        {
            transform.position = new Vector3(transform.position.x , transform.position.y,- 34);
        }
    }
}
