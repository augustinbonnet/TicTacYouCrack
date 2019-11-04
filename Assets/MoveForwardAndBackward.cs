using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardAndBackward : MonoBehaviour
{
    public float timer = 0;
    public float Step = 0.002f;
    private bool Forward = true;

    private void Update()
    {
        timer += Time.deltaTime;
        if (transform.position.z < -73.19f && Forward)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, Step) * Time.fixedDeltaTime * 10000);
            Forward = false;
        }
        if(transform.position.z > -60f && !Forward)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -Step) * Time.fixedDeltaTime * 10000);
            Forward = true;
        }
    }
}
