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
    public Transform CameraTransform;
    public GameController GC;
    public float SlowDownFactor = 5f;
    public bool IsSlowingDown = false;
    public float AccelerationFactor = 1.3f;
    public bool IsAccelerated = false;

    public int jumpNumber = 0;
    private bool spacePressed = false;

    public float Timer = 0;
    public List<string> CollisionList;
    public float Grav = 10f;
    private Transform GOTemp;

    private void Start()
    {
        CollisionList = new List<string>();
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        GC.SpawnPlayer();
        GOTemp = transform.parent;
    }

    void OnCollisionEnter(Collision col)
    {
        CollisionList.Add(col.gameObject.name);
        Timer = 0;
        if (col.gameObject.tag == ("Ground") && isGrounded == false)
        {
            isGrounded = true;
            
        }
        if(col.gameObject.name == "PlaneMoving")
        {
            gameObject.transform.SetParent(col.transform.parent);
            
            //transform.localScale = new Vector3(1, 1, 1);
            //transform.position += col.gameObject.GetComponent<Rigidbody>().velocity;
            //Debug.Log(col.gameObject.GetComponent<Rigidbody>().velocity);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "PlaneMoving")
        {
            //gameObject.GetComponent<Rigidbody>().MovePosition(collision.transform.position + transform.right * Time.fixedDeltaTime);
        }
        Timer = 0;
    }

    private void OnCollisionExit(Collision collision)
    {
        Timer = 0;
        CollisionList.Remove(collision.gameObject.name);
        if (collision.gameObject.name == "PlaneMoving")
        {
            gameObject.transform.SetParent(GOTemp);
            //transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Update()
    {
        Physics.gravity = new Vector3(0, Gravity * 10, 0);
        Timer += Time.deltaTime;
        if (Timer)
        if (Timer > 1 && CollisionList.Count == 0)
        {
            GC.PlayerDie();
            Timer = 0;
        }
        if (CollisionList.Contains("PlaneMoving"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0f, -5000f * Time.deltaTime * Grav, 0f));
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
        if (Input.GetKey("left shift") && !IsAccelerated)
        {
            IsAccelerated = true;
            sideWayForce *= AccelerationFactor;
            forwardForce *= AccelerationFactor;
        }else if (IsAccelerated)
        {
            sideWayForce /= AccelerationFactor;
            forwardForce /= AccelerationFactor;
            IsAccelerated = false;
        }
        if (Input.GetKey("q") && Input.GetKey("z") && !IsSlowingDown || Input.GetKey("d") && Input.GetKey("z") && !IsSlowingDown || Input.GetKey("q") && Input.GetKey("s") && !IsSlowingDown || Input.GetKey("d") && Input.GetKey("z") && !IsSlowingDown)
        {
            IsSlowingDown = true;
            sideWayForce /= SlowDownFactor;
            forwardForce /= SlowDownFactor;
        }else if (IsSlowingDown)
        {
            IsSlowingDown = false;
            forwardForce *= SlowDownFactor;
            sideWayForce *= SlowDownFactor;
        }
        if (Input.GetKey("a"))
        {
            if (MovableObject.CurrentObjectSelected != null && MovableObject.CurrentObjectSelected.GetComponent<MovableObject>().Movable && MovableObject.CurrentObjectSelected.GetComponent<Rigidbody>().velocity.magnitude <12)
            {
                Vector3 PullMovement = new Vector3(transform.position.x - MovableObject.CurrentObjectSelected.transform.position.x, 0, transform.position.z - MovableObject.CurrentObjectSelected.transform.position.z).normalized * Time.deltaTime;
                MovableObject.CurrentObjectSelected.GetComponent<Rigidbody>().AddForce(PullMovement * MovableObject.CurrentObjectSelected.GetComponent<MovableObject>().MagnetSpeed * 1000);
            }
        }
        if (Input.GetKey("e"))
        {
            if (MovableObject.CurrentObjectSelected != null && MovableObject.CurrentObjectSelected.GetComponent<MovableObject>().Movable && MovableObject.CurrentObjectSelected.GetComponent<Rigidbody>().velocity.magnitude < 12)
            {
                Vector3 PushMovement = new Vector3(transform.position.x - MovableObject.CurrentObjectSelected.transform.position.x, 0, transform.position.z - MovableObject.CurrentObjectSelected.transform.position.z).normalized * Time.deltaTime;
                MovableObject.CurrentObjectSelected.GetComponent<Rigidbody>().AddForce(-PushMovement * MovableObject.CurrentObjectSelected.GetComponent<MovableObject>().MagnetSpeed * 1000);
            }
        }
    }
}