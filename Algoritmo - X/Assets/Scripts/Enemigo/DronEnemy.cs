using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronEnemy : MonoBehaviour
{  
    public GameObject disparo;
    public Transform disparoSpawn;
    public float delay;
    public float velocidadDisparo;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("fuego", delay, velocidadDisparo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fuego()
    {
        Instantiate(disparo, disparoSpawn.position, disparoSpawn.rotation);
    }
}
