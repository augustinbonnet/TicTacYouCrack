using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareLevel : MonoBehaviour
{

    public int Level = 1;
    public Level01 LVL1Script;

    void Update()
    {
    }

    public void LevelUp()
    {
        if (Level == 1)
        {
            LVL1Script.Enable = false;
        }
        Level++;
    }
}
