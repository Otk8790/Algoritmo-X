using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVida : MonoBehaviour
{
    public int currentHealth;
    public ParticleSystem explosionParticle;
    /* public AudioSource Destruir;
    public GameObject SonidoDestruir; */

    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
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
            muerto();
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject,2f);
        }
    }
    public void muerto()
    {
        anim.SetTrigger("muerto");
    }
}
