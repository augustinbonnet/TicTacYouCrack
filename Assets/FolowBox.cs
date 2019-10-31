using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowBox : MonoBehaviour
{
    public Transform Box;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (Box.position.x, transform.position.y, Box.position.z);
    }
}
