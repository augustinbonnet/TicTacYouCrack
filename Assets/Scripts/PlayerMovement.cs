using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    public float Gravity = -15f;

    public bool CanMove = false;
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

    public bool IsMagneted = false;    

    private void Start()
    {
        CollisionList = new List<string>();
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
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
        }
        if (col.gameObject.name == "Rock")
        {
            gameObject.transform.SetParent(col.transform.parent.parent);
        }
        if (col.gameObject.tag == "DeadSphere" && !GC.FinishedStage)
        {
            GC.PlayerDie();
        }
        if (col.gameObject.name == "FinishedPlatform")
        {
            GC.FinishStage();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Timer = 0;
    }

    private void OnCollisionExit(Collision collision)
    {
        Timer = 0;
        CollisionList.Remove(collision.gameObject.name);
        if (collision.gameObject.name == "PlaneMoving" || collision.gameObject.name == "Rock")
        {
            gameObject.transform.SetParent(GOTemp);
        }
    }

    private void Update()
    {
        Physics.gravity = new Vector3(0, Gravity * 10, 0);
        Timer += Time.deltaTime;
        if (Timer > 1 && CollisionList.Count == 0 && !IsMagneted)
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
        if (CanMove)
        {
            if (Input.GetKey("z"))
            {
                Vector3 ForwardMovement = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized * Time.deltaTime;
                rb.AddForce(ForwardMovement * forwardForce * 1000, ForceMode.Force);
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
            if (Input.GetKey("space") && isGrounded && !spacePressed && CollisionList.Count > 0)
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
            }
            else
            {
                spacePressed = false;
            }
            if (Input.GetKey("left shift") && !IsAccelerated)
            {
                IsAccelerated = true;
                sideWayForce *= AccelerationFactor;
                forwardForce *= AccelerationFactor;
            }
            else if (IsAccelerated)
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
            }
            else if (IsSlowingDown)
            {
                IsSlowingDown = false;
                forwardForce *= SlowDownFactor;
                sideWayForce *= SlowDownFactor;
            }
            if (Input.GetKey("a"))
            {
                if (MovableObject.CurrentObjectSelected != null && MovableObject.CurrentObjectSelected.GetComponent<MovableObject>().Movable && MovableObject.CurrentObjectSelected.GetComponent<Rigidbody>().velocity.magnitude < 12)
                {
                    MovableObject.CurrentObjectSelected.GetComponent<MovableObject>().Pull(transform);
                }
            }
            if (Input.GetKey("e"))
            {
                if (MovableObject.CurrentObjectSelected != null && MovableObject.CurrentObjectSelected.GetComponent<MovableObject>().Movable && MovableObject.CurrentObjectSelected.GetComponent<Rigidbody>().velocity.magnitude < 12)
                {
                    MovableObject.CurrentObjectSelected.GetComponent<MovableObject>().Push(transform);
                }
            }
        }
    }
}