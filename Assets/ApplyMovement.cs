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
    public GameObject VFXHitBall;
    public float Power = 1;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1000 * Height, 5000 * Speed));
        transform.position = StartingPosition.position;
        Vector3 move = new Vector3(BallDirection.x, BallDirection.y * Height, BallDirection.z * Speed);
        gameObject.GetComponent<Rigidbody>().AddForce(Physics.gravity * gameObject.GetComponent<Rigidbody>().mass * Time.fixedDeltaTime + move);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LifeTime -= Time.deltaTime;
        gameObject.GetComponent<Rigidbody>().AddForce(0, Gravity * Time.fixedDeltaTime * 1000, 0);
        if (LifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<AudioSource>().Play();
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(BallDirection.x, BallDirection.y*10, BallDirection.z)*Time.deltaTime * Power);
            Instantiate(VFXHitBall, transform);
        }
    }
}
