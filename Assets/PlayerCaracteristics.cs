using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCaracteristics : MonoBehaviour
{
    public int lives = 0;
    public Text UILifeNumber;

    public int Dies(){
        lives++;
        UILifeNumber.text = lives.ToString();
        return lives;
    }
}
