using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject UILevel5;

    private bool DidIt = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !DidIt)
            UILevel5.SetActive(true);
            DidIt = true;
    }
}
