using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullerController : MonoBehaviour
{
    public float moveSpeed, lifeTime;
    public Rigidbody theRB;
    public int damage;
    public ParticleSystem explosionParticle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.forward * moveSpeed;

        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
            other.gameObject.GetComponent<EnemyVida>().DamageEnemy(damage);
        }
        else if (other.gameObject.tag == "Drone")
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
            other.gameObject.GetComponent<EnemyVidaDron>().DamageEnemy(damage);
        }
    }
}
