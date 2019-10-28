using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyMovement : MonoBehaviour
{

    public Transform StartingPosition;
    public float Speed = 86.63f;
    public float Height = 159;
    public float Gravity = -4000;
    public float LifeTime = 1.3f;
    public Vector3 BallDirection;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1000 * Height, 5000 * Speed));
        transform.position = StartingPosition.position;
        Vector3 move = new Vector3(BallDirection.x, BallDirection.y * Height, BallDirection.z * Speed);
        gameObject.GetComponent<Rigidbody>().AddForce(Physics.gravity * gameObject.GetComponent<Rigidbody>().mass + move);
    }

    // Update is called once per frame
    void Update()
    {
        LifeTime -= Time.deltaTime;
        gameObject.GetComponent<Rigidbody>().AddForce(0, Gravity, 0);
        if (LifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(BallDirection.x, BallDirection.y*4, BallDirection.z));
        }
    }
}
