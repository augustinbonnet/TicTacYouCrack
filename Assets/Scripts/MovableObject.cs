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

    private void Update()
    {
        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 20)
        {
            Destroy(gameObject);
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
            Movable = true;
        }
    }

}
