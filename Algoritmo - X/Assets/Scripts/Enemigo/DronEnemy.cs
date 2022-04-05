using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronEnemy : MonoBehaviour
{  
    [Header("DISPARO ENEMIGO")]
    public GameObject disparo;
    public Transform disparoSpawn;
    public float delay;
    public float velocidadDisparo;

    [Header("SEGUIR ENEMIGO")]
    public float rangoAlerta;
    public LayerMask capaDeHope;
    private bool estarAlerta;
    public Transform player;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("fuego", delay, velocidadDisparo);
    }

    // Update is called once per frame
    void Update()
    {
        estarAlerta = Physics.CheckSphere(transform.position, rangoAlerta, capaDeHope);
        if (estarAlerta == true)
        {
            //transform.LookAt(player)
            transform.LookAt(new Vector3 (player.position.x, transform.position.y, player.position.z));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, player.position.y, player.position.z), speed * Time.deltaTime);
            //transform.LookAt(new Vector3(player.rotation.x, transform.rotation.y, player.rotation.z));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, rangoAlerta);
    }

    void fuego()
    {
        Instantiate(disparo, disparoSpawn.position, disparoSpawn.rotation);
    }
}
