using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public static GameObject CurrentObjectSelected;
    public Material StandardMaterial;
    public Material SelectedMaterial;
    public Transform PlayerPosition;
    public bool Movable = false;
    public float MagnetSpeed = 10;
    public bool ShouldDisapear = true;
    public bool Vertical = false;

    private float Timer = 0.7f;
    private bool StartTimer = false;
    private bool IsTouchingPlayer = false;
    public bool HasAGroundOnTop = false;

    private void Update()
    {
        if (StartTimer)
        {
            Timer -= Time.deltaTime;
        }
        if (Timer > 0 && Timer != 0.7f || IsTouchingPlayer)
        {
            Movable = false;
        }
        else
        {
            Movable = true;
            StartTimer = false;
            Timer = 0.7f;
        }

        if (!HasAGroundOnTop)
        {
            if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 25 && ShouldDisapear)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (transform.GetComponent<Rigidbody>().velocity.magnitude > 25 && ShouldDisapear)
            {
                Destroy(gameObject);
            }
        }
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (CurrentObjectSelected != null)
            {
                CurrentObjectSelected.GetComponent<Renderer>().material = StandardMaterial;
                Movable = false;
            }
            gameObject.GetComponent<Renderer>().material = SelectedMaterial;
            CurrentObjectSelected = gameObject;
            IsTouchingPlayer = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartTimer = true;
            IsTouchingPlayer = false;
        }
    }

    public void Pull(Transform PlayerTransform)
    {
        if (!CurrentObjectSelected.GetComponent<MovableObject>().Vertical)
        {
            PullTowardsPlayer(PlayerTransform);
        }
        else
        {
            PullDown();
        }
    }

    public void Push(Transform PlayerTransform)
    {
        if (!CurrentObjectSelected.GetComponent<MovableObject>().Vertical)
        {
            PushFromPlayer(PlayerTransform);
        }
        else
        {
            PullUp();
        }
    }

    private void PullTowardsPlayer(Transform PlayerTransform)
    {
        if (!HasAGroundOnTop)
        {
            Vector3 PullMovement = new Vector3(PlayerTransform.position.x - gameObject.transform.position.x, 0, PlayerTransform.position.z - gameObject.transform.position.z).normalized * Time.deltaTime;
            gameObject.GetComponent<Rigidbody>().AddForce(PullMovement * MagnetSpeed * 1000);
        }
        else
        {
            Vector3 PullMovement = new Vector3(PlayerTransform.position.x - gameObject.transform.position.x, 0, PlayerTransform.position.z - gameObject.transform.position.z).normalized * Time.deltaTime;
            transform.position += PullMovement * MagnetSpeed * 0.01f;
        }
    }

    private void PushFromPlayer(Transform PlayerTransform)
    {
        if (!HasAGroundOnTop)
        {
            Vector3 PushMovement = new Vector3(PlayerTransform.position.x - transform.position.x, 0, PlayerTransform.position.z - transform.position.z).normalized * Time.deltaTime;
            gameObject.GetComponent<Rigidbody>().AddForce(-PushMovement * MagnetSpeed * 1000);
        }
        else
        {
            Vector3 PullMovement = new Vector3(PlayerTransform.position.x - gameObject.transform.position.x, 0, PlayerTransform.position.z - gameObject.transform.position.z).normalized * Time.deltaTime;
            transform.position -= PullMovement * MagnetSpeed * 0.01f;
        }
            
    }

    public void PullUp()
    {
        if (transform.parent.position.y < 21f && Movable)
        {
            transform.parent.position += new Vector3(0f, 4f, 0f) * Time.fixedDeltaTime;
        }
    }
    public void PullDown()
    {
        if (transform.parent.position.y > 16f && Movable)
        {
            transform.parent.position += new Vector3(0f, -4f, 0f) * Time.fixedDeltaTime;
        }
    }
}
