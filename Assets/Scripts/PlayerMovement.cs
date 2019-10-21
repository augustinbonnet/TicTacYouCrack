using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    public float Gravity = -15f;

    private bool isGrounded;
    private Vector3 jump;
    public float jumpForce = 2f;
    public float forwardForce = 2000f;
    public float sideWayForce = 600f;
    public Transform RestartPoint;
    public PlayerCaracteristics Player;
    public Transform CameraTransform;

    public int jumpNumber = 0;

    private bool spacePressed = false;

    public float distance = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
	    Player = GetComponent<PlayerCaracteristics>();
        transform.position = RestartPoint.position;
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
        Physics.gravity = new Vector3(0, Gravity * 10, 0);
        if (transform.position.y <= -2)
        {
            gameObject.transform.position = RestartPoint.position;
            gameObject.transform.rotation = RestartPoint.rotation;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Player.Dies();
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey("z"))
        {
            Vector3 ForwardMovement = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized * Time.deltaTime;
            rb.AddForce(ForwardMovement * forwardForce*1000, ForceMode.Force);
            //transform.position = transform.position + Camera.main.transform.forward * distance * Time.deltaTime;
        }
        if (Input.GetKey("q"))
        {
            Vector3 LeftMovement = Vector3.Scale(-Camera.main.transform.right, new Vector3(1, 0, 1)).normalized * Time.deltaTime;
            rb.AddForce(LeftMovement * sideWayForce * 1000);
        }
        if (Input.GetKey("s"))
        {
            Vector3 Backwardmovement = Vector3.Scale(-Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized * Time.deltaTime;
            rb.AddForce(Backwardmovement * forwardForce * 1000);
        }
        if (Input.GetKey("d"))
        {
            Vector3 RightMovement = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized * Time.deltaTime;
            rb.AddForce(RightMovement * sideWayForce * 1000);
        }
        if (Input.GetKey("space") && isGrounded && !spacePressed)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpNumber++;
            spacePressed = true;
        }
        else
        if (Input.GetKey("space"))
        {
            spacePressed = true;
        }else
        {
            spacePressed = false;
        }
        if (rb.velocity.magnitude > 9)
        {
            Debug.Log("Magnitude 1 : " + rb.velocity.magnitude);
            rb.velocity.Normalize();
            Debug.Log("Magnitude 2 : " + rb.velocity.magnitude);
        }
    }
}
