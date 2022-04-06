using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyVidaDron : MonoBehaviour
{
    public int currentHealth = 3;
    public ParticleSystem explosionParticle;
    public Slider BarraVidaEnemigo;
    /* public AudioSource Destruir;
    public GameObject SonidoDestruir; */

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BarraVidaEnemigo.value = currentHealth;
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
