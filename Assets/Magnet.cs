using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{

    public GameObject Player;
    public float GrabArea = 5;
    public float Magnetpower = 5;

    private void FixedUpdate()
    {
        if (Input.GetKey("a"))
        {
            if (Vector3.Distance(Player.transform.position, transform.position) < GrabArea)
            {
                Player.GetComponent<PlayerMovement>().IsMagneted = true;
                Vector3 DirectionToGetPulled = (transform.position - Player.transform.position) * Time.fixedDeltaTime * 10000 * Magnetpower;
                Player.GetComponent<Rigidbody>().AddForce(DirectionToGetPulled);
                Player.GetComponent<PlayerMovement>().Timer = 0;
            }
            else
            {
                Player.GetComponent<PlayerMovement>().IsMagneted = false;
            }
        }
    }
}
