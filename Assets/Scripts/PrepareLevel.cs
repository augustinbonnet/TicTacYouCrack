﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PrepareLevel : MonoBehaviour
{

    public int Level = 0;
    public Level00 LVL0Script;
    public Level01 LVL1Script;
    public Level02 LVL2Script;
    public Level03 LVL3Script;
    public Level04 LVL4Script;
    public Level05 LVL5Script;
    public Level06 LVL6Script;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        LVL0Script.Enable();
        /*
        LVL1Script.Disable();
        LVL5Script.Enable();
        Level = 5;*/
    }

    public void LevelUp()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if (Level == 0)
        {
            LVL0Script.Disable();
            LVL1Script.Enable();
        }else if (Level == 1)
        {
            LVL1Script.Disable();
            LVL2Script.Enable();
        }else if (Level == 2)
        {
            LVL2Script.Disable();
            LVL3Script.Enable();
        }
        else if (Level == 3)
        {
            LVL3Script.Disable();
            LVL4Script.Enable();
        }
        else if (Level == 4)
        {
            LVL4Script.Disable();
            LVL5Script.Enable();
        }else if (Level == 5)
        {
            LVL5Script.Disable();
            LVL6Script.Enable();
        }
        Level++;
    }
}
