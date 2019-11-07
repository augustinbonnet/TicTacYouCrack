using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameController GC;

    public void DoSpawnPlayer()
    {
        GC.SpawnPlayer();
        Destroy(gameObject);
    }
}
