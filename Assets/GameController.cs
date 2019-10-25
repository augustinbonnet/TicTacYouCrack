using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public PlayerCaracteristics PlayerCaracs;
    public Transform FirstLevelPos;
    public Transform PositionToTP;
    public int CurrrentLevel = 1;
    public Transform Lvl2TPPosition;
    public Transform Lvl3TPPosition;
    public Text UILevel;
    public GameObject ThrowingBall;
    private float Timer = 9999;
    private int tmp = 0;

    private void Start()
    {
        PositionToTP = FirstLevelPos;
        PlayerCaracs = Player.GetComponent<PlayerCaracteristics>();
        UILevel.text = "1";
    }

    private void Update()
    {
        Timer -= Time.deltaTime;
        if ((int)Timer % 1 == 0 && tmp != (int) Timer )
        {
            Instantiate(ThrowingBall);
            tmp = (int) Timer;
        }
        if (Player.transform.position.y <= -2)
        {
            Player.transform.position = PositionToTP.position;
            Player.transform.rotation = PositionToTP.rotation;
            Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            PlayerCaracs.Dies();
        }

        if (Vector3.Distance(Player.transform.position, Lvl2TPPosition.position) < 4.5f && CurrrentLevel < 2)
        {
            Debug.Log("CP 2");
            CurrrentLevel = 2;
            PositionToTP = Lvl2TPPosition;
            UILevel.text = CurrrentLevel.ToString();
        }

        if (Vector3.Distance(Player.transform.position, Lvl3TPPosition.position) < 4.5f && CurrrentLevel < 3)
        {
            Debug.Log("CP 3");
            CurrrentLevel = 3;
            PositionToTP = Lvl3TPPosition;
            UILevel.text = CurrrentLevel.ToString();
        }
    }

    public void SpawnPlayer()
    {
        Player.transform.position = FirstLevelPos.position;
    }
}
