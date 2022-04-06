using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnpoint;

    public void Start()
    {
        InvokeRepeating("spawn", spawnTime, spawnTime);

    }
    public void Update()
    {
        
    }
    void spawn()
    {
        if (playerController.vida <= 0)
        {
            return;
        }
        int spawnIndex = Random.Range(0, spawnpoint.Length);
        Instantiate(enemy, spawnpoint[spawnIndex].position, spawnpoint[spawnIndex].rotation);
    }
}
