using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Card[] prefabs;
    public GameObject[] spawnPoints;

    public Card Spawn()
    {
        return prefabs[Random.Range(0, prefabs.Length)];
    }
}
