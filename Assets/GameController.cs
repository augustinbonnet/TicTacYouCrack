using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour
{
    public GameObject Player;
    public PlayerCaracteristics PlayerCaracs;
    public Transform FirstLevelPos;
    public Transform PositionToTP;
    public int CurrrentLevel = 1;
    public Transform Lvl2TPPosition;
    public Transform Lvl3TPPosition;
    public Transform Lvl4TPPosition;
    public Transform Lvl5TPPosition;
    public Transform Lvl6TPPosition;
    public Text UILevel;
    public Text UITimer;
    public GameObject VFXHitThrowingBall;
    public GameObject VFXCheckPoint;
    public PrepareLevel PrepLevel;
    public float DeathTime = 1;
    public float Timer = 0f;

    public AudioClip impact;
    AudioSource audioSource;

    public bool FinishedStage = false;
    public GameObject VFXEndingStage;
    public GameObject UIEndingStage;
    public Text TextUIEndingStage;


    private void Start()
    {
        PositionToTP = FirstLevelPos;
        PlayerCaracs = Player.GetComponent<PlayerCaracteristics>();
        UILevel.text = "1";
        audioSource = GetComponent<AudioSource>();

        PositionToTP = Lvl5TPPosition;
        CurrrentLevel = 5;
    }

    private void Update()
    {
        if (!FinishedStage)
        {
            Timer += Time.deltaTime;
            UITimer.text = ((int)Timer).ToString();
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
            PrepLevel.LevelUp();
            CurrrentLevel = 3;
            PositionToTP = Lvl3TPPosition;
            UILevel.text = CurrrentLevel.ToString();
        }

        if (Vector3.Distance(Player.transform.position, Lvl4TPPosition.position) < 4.5f && CurrrentLevel < 4)
        {
            PrepLevel.LevelUp();
            CurrrentLevel = 4;
            PositionToTP = Lvl4TPPosition;
            UILevel.text = CurrrentLevel.ToString();
        }
        if (Vector3.Distance(Player.transform.position, Lvl5TPPosition.position) < 3.5f && CurrrentLevel < 5)
        {
            PrepLevel.LevelUp();
            CurrrentLevel = 5;
            PositionToTP = Lvl5TPPosition;
            UILevel.text = CurrrentLevel.ToString();
        }
        if (Vector3.Distance(Player.transform.position, Lvl6TPPosition.position) < 3.5f && CurrrentLevel < 6)
        {
            PrepLevel.LevelUp();
            CurrrentLevel = 6;
            PositionToTP = Lvl6TPPosition;
            UILevel.text = CurrrentLevel.ToString();
        }
    }

    public void SpawnPlayer()
    {
        Player.transform.position = FirstLevelPos.position;
    }

    public void PlayerDie()
    {
        StartCoroutine(WaitForTwoSeconds());
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
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        Player.SetActive(false);
        Instantiate(VFXHitThrowingBall).SetActive(true);
        yield return new WaitForSeconds(DeathTime);
        Player.SetActive(true);
        ResetPostion();
    }

    public void FinishStage()
    {
        FinishedStage = true;
        VFXEndingStage.SetActive(true);
        TextUIEndingStage.text = "Congratulations, you finished the stage 1  in " + (int) Timer + " seconds and " + PlayerCaracs.lives + " lives !";
        UIEndingStage.SetActive(true);
        Player.transform.position = FirstLevelPos.position;
    }
}
