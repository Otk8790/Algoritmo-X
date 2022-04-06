using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronEnemy : MonoBehaviour
{  
    [Header("DISPARO ENEMIGO")]
    public GameObject bullet;
    public Transform disparoSpawn;
    public float EnemRate = 4.0f;
    private float EnemRateTime = 0f;
    public float shotForce = 1500f;

    [Header("SEGUIR ENEMIGO")]
    public float rangoAlerta;
    public LayerMask capaDeHope;
    public bool estarAlerta;
    public Transform player;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        estarAlerta = Physics.CheckSphere(transform.position, rangoAlerta, capaDeHope);
        if (estarAlerta == true)
        {
            transform.LookAt(new Vector3 (player.position.x, transform.position.y, player.position.z));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, player.position.y, player.position.z), speed * Time.deltaTime);
            fuego();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoAlerta);
    }

    public void fuego()
    {
        if (Time.time > EnemRateTime)
        {
            GameObject newBullet;
            newBullet = Instantiate(bullet, disparoSpawn.position, disparoSpawn.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(disparoSpawn.forward * shotForce);
            Destroy(newBullet, 1);
            EnemRateTime = Time.time + EnemRate;
        }
    }
}
