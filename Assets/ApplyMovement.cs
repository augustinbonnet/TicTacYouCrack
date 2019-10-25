using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyMovement : MonoBehaviour
{

    public Transform StartingPosition;
    public float Speed = 1f;
    public float Height = 1;
    public float Gravity = 15;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1000 * Height, 5000 * Speed));
        Vector3 move = new Vector3(0, 1000 * Height, 5000 * Speed);
        gameObject.GetComponent<Rigidbody>().AddForce(Physics.gravity * gameObject.GetComponent<Rigidbody>().mass + move);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(0, Gravity, 0);
        if (transform.position.z > 10)
        {
            Destroy(gameObject);
        }
    }
}
