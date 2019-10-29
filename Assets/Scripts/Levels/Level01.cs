using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01 : MonoBehaviour
{

    public GameObject ThrowingBall1;
    public GameObject ThrowingBall2;
    public GameObject VFXCheckPoint;
    private float Timer = 9999;
    private int tmp = 0;

    private void Update()
    {
        Timer -= Time.deltaTime;
        if ((int)Timer % 1 == 0 && tmp != (int)Timer)
        {
            Instantiate(ThrowingBall1);
            Instantiate(ThrowingBall2);
            tmp = (int)Timer;
        }
    }

    public void Disable()
    {
        VFXCheckPoint.SetActive(false);
        Destroy(this);
    }
}
