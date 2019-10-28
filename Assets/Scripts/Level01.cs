using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01 : MonoBehaviour
{

    public GameObject ThrowingBall1;
    public GameObject ThrowingBall2;
    public GameObject VFXCheckPoint;
    public bool Enable = true;
    private float Timer = 9999;
    private int tmp = 0;

    private void Update()
    {
        Timer -= Time.deltaTime;
        if ((int)Timer % 1 == 0 && tmp != (int)Timer && Enable)
        {
            Instantiate(ThrowingBall1);
            Instantiate(ThrowingBall2);
            tmp = (int)Timer;
        }
        if (!Enable)
        {
            Debug.Log("Disable");
            VFXCheckPoint.SetActive(false);
            Destroy(this);
        }
    } 
}
