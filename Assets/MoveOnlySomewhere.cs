using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnlySomewhere : MonoBehaviour
{
    public float ComeBackForce = 2000;

    void Update()
    {
        if (transform.position.z > -34.2f)
        {
            transform.parent.GetComponent<Rigidbody>().AddForce(-new Vector3(0, 0, ComeBackForce) * Time.deltaTime);
        }
    }
}
