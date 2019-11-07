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
    public int CurrrentLevel = 0;
    public Transform Lvl1TPPosition;
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

    private bool FreezePosition = false;
    private bool PlayerIsDying = false;
    public GameObject FirstVFX;
    private bool PlayerIsSpawned = false;

    //public GameObject 


    private void Start()
    {
        PositionToTP = FirstLevelPos;
        PlayerCaracs = Player.GetComponent<PlayerCaracteristics>();
        UILevel.text = "0";
        audioSource = GetComponent<AudioSource>();

        //GC.SpawnPlayer();

        /*
        PositionToTP = Lvl5TPPosition;
        CurrrentLevel = 5;*/
    }

    private void Update()
    {
        if (FreezePosition)
        {
            Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            Player.GetComponent<Rigidbody>().useGravity = false;
        }

        if (!FinishedStage)
        {
            Timer += Time.deltaTime;
            UITimer.text = ((int)Timer).ToString();
        }
        if (Vector3.Distance(Player.transform.position, Lvl1TPPosition.position) < 4.5f && CurrrentLevel < 1 && PlayerIsSpawned)
        {
            PrepLevel.LevelUp();
            CurrrentLevel = 1;
            PositionToTP = Lvl1TPPosition;
            UILevel.text = CurrrentLevel.ToString();
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
        PlayerIsSpawned = true;
        FirstVFX.SetActive(true);
        Player.transform.position = FirstLevelPos.position;
        Player.GetComponent<PlayerMovement>().CanMove = true;
    }

    public void PlayerDie()
    {
        if (!PlayerIsDying)
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
        PlayerIsDying = true;
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        Player.GetComponent<MeshRenderer>().enabled = false;
        FreezePosition = true;

        Instantiate(VFXHitThrowingBall).SetActive(true);
        yield return new WaitForSeconds(DeathTime);
        ResetPostion();
        FreezePosition = false;
        Player.GetComponent<Rigidbody>().useGravity = true;
        Player.GetComponent<MeshRenderer>().enabled = true;
        PlayerIsDying = false;
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
