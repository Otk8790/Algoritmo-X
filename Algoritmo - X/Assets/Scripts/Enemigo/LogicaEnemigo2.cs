using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicaEnemigo2 : MonoBehaviour {
    
    public int hp;
    public int dañoBaston;
    public Animator anim;
    public Slider BarraVidaEnemigo;
    public ParticleSystem explosionParticle;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		BarraVidaEnemigo.value = hp;
	}
    public void muerto()
    {
        anim.SetTrigger("muerto");
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "item")
        {
            if(anim != null)
            {
                anim.SetTrigger("DañoEnemigo");
            }
            hp -= dañoBaston;
        }

        if(hp <= 0)
        {
            muerto();
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject,2f);
        }
    }
}
