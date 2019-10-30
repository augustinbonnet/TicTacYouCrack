using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level04 : MonoBehaviour
{
    public GameObject VFXCheckPoint;
    public GameObject UIInstructionToDisplay;

    public void Disable()
    {
        VFXCheckPoint.SetActive(false);
        Destroy(this);
    }

    public void Enable()
    {
        VFXCheckPoint.SetActive(true);
        UIInstructionToDisplay.SetActive(true);
    }
}
