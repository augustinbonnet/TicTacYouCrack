using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    private bool isGrounded;
    private Vector3 jump;
    public float jumpForce = 2f;
    public float forwardForce = 2000f;
    public float sideWayForce = 600f;
    public Transform RestartPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ("Ground") && isGrounded == false)
        {
            isGrounded = true;
        }
    }

    private void Update()
    {
        if (transform.position.y <= -2)
        {
            gameObject.transform.position = RestartPoint.position;
            gameObject.transform.rotation = RestartPoint.rotation;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey("z"))
        {
            rb.AddForce(new Vector3(-forwardForce * Time.deltaTime, 0, 0), ForceMode.Force);
        }
        if (Input.GetKey("q"))
        {
            rb.AddForce(new Vector3(0, 0, -sideWayForce * Time.deltaTime));
        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(new Vector3(forwardForce * Time.deltaTime, 0));
        }
        if (Input.GetKey("d"))
        {
            rb.AddForce(new Vector3(0, 0, sideWayForce * Time.deltaTime));
        }
        if (Input.GetKey("space") && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }        
    }
}
