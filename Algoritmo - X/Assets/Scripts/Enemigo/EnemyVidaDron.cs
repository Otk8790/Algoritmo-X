using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVidaDron : MonoBehaviour
{
    public int currentHealth = 5;
    public ParticleSystem explosionParticle;
    /* public AudioSource Destruir;
    public GameObject SonidoDestruir; */

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageEnemy(int damageAmount)
    {
        
        currentHealth -= damageAmount;
        /* Instantiate(SonidoDestruir); */

        if(currentHealth <= 0)
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
        }
    }
}
