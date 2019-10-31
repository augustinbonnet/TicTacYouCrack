using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnlySomewhere : MonoBehaviour
{
    public float ComeBackForce = 3000;

    void Update()
    {
        if (transform.position.z > -34.2)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(-new Vector3(0, 0, ComeBackForce) * Time.deltaTime);
        }
    }
}
