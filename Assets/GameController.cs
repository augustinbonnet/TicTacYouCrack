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
    public GameObject VFXHitThrowingBall;
    public GameObject VFXCheckPoint;
    public PrepareLevel PrepLevel;
    public float DeathTime = 1;

    private bool PlayerIsDying = false;

    private void Start()
    {
        PositionToTP = FirstLevelPos;
        PlayerCaracs = Player.GetComponent<PlayerCaracteristics>();
        UILevel.text = "1";
    }

    private void Update()
    {
        if (Player.transform.position.y <= -2 && !PlayerIsDying)
        {
            PlayerIsDying = true;
            StartCoroutine(WaitForTwoSeconds());
        }

        if (Vector3.Distance(Player.transform.position, Lvl2TPPosition.position) < 4.5f && CurrrentLevel < 2)
        {
            PrepLevel.LevelUp();
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

    public void PlayerDie()
    {
        Player.SetActive(true);
        ResetPostion();
    }

    public void ResetPostion()
    {
        Player.transform.position = PositionToTP.position;
        Player.transform.rotation = PositionToTP.rotation;
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        PlayerCaracs.Dies();
    }

    IEnumerator WaitForTwoSeconds()
    {
        Player.SetActive(false);
        Instantiate(VFXHitThrowingBall).SetActive(true);
        yield return new WaitForSeconds(DeathTime);
        PlayerDie();
        PlayerIsDying = false;
    }
}
