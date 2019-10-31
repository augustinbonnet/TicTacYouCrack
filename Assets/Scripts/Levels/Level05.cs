using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level05 : MonoBehaviour
{
    public GameObject VFXCheckPoint;

    public void Disable()
    {
        VFXCheckPoint.SetActive(false);
        Destroy(this);
    }

    public void Enable()
    {
        VFXCheckPoint.SetActive(true);
    }
}
