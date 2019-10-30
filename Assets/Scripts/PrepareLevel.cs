using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PrepareLevel : MonoBehaviour
{

    public int Level = 1;
    public Level01 LVL1Script;
    public Level02 LVL2Script;
    public Level03 LVL3Script;
    public Level04 LVL4Script;

    public AudioClip CheckPointSound;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        LVL1Script.Disable();
        LVL4Script.Enable();
        Level = 4;
    }

    public void LevelUp()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if (Level == 1)
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
        Level++;
    }
}
