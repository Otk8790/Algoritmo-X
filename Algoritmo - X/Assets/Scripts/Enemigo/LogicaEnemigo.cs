using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LogicaEnemigo : MonoBehaviour {
     private GameObject target;
    private NavMeshAgent agente;
    private Vida vida;
    private Animator animator;
    private Collider collide;
    private Vida vidaJugador;
    public PlayerController playerController;
    public bool Vida0 = false;
    public bool estaAtacando = false;
    public float speed = 1.0f;
    public float angularSpeed = 120;
    public float daño = 1;
   
    public bool mirando;

	// Use this for initialization
	void Start () {
        target = GameObject.Find("Player");
        vidaJugador = target.GetComponent<Vida>();
        if(playerController== null)
        {
            throw new System.Exception("El objeto Jugador no tiene componente Vida");
        }

        playerController = target.GetComponent<PlayerController>();

        if (playerController == null)
        {
            throw new System.Exception("El objeto Jugador no tiene componente LogicaJugador");
        }

        agente = GetComponent<NavMeshAgent>();
        vida = GetComponent<Vida>();
        animator = GetComponent<Animator>();
        collide = GetComponent<Collider>();
        

    }
	
	// Update is called once per frame
	void Update () {
        //RevisarVida();
        Perseguir();
        RevisarAtaque();
        EstaDefrenteAlJugador();
	}

    void EstaDefrenteAlJugador()
    {
        Vector3 adelante = transform.forward;
        Vector3 targetJugador = (GameObject.Find("Player").transform.position - transform.position).normalized;

        if(Vector3.Dot(adelante,targetJugador)> 5f)
        {
            mirando = false;
        }
        else
        {
            mirando = true;
        }
    }

    /*void RevisarVida()
    {
        if (Vida0) return;
        if(vida.valor <= 0)
        {
            Vida0 = true;
            agente.isStopped = true;
            collide.enabled = false;
            animator.CrossFadeInFixedTime("Vida0", 0.1f);
            Destroy(gameObject, 3f);
        }

    }*/

    void Perseguir()
    {
        if (Vida0) return;
//        if (logicaJugador.Vida0) return;
        agente.destination = target.transform.position;
    }

    void RevisarAtaque()
    {
        if (Vida0) return;
        if (estaAtacando) return;
//        if (logicaJugador.Vida0) return;
        float distanciaDelBlanco = Vector3.Distance(target.transform.position, transform.position);

        if(distanciaDelBlanco <= 2.0 && mirando)
        {
            Atacar();
        }   
    }

    void Atacar()
    {
        //vidaJugador.RecibirDaño(daño);
        if(playerController.vida <= 0)
        {
            Debug.Log("no ataco");
        }
        else
        {
            playerController.dañozombi();
            agente.speed = 0;
            agente.angularSpeed = 0;
            estaAtacando = true;
            animator.SetTrigger("DebeAtacar");
            Invoke("ReiniciarAtaque", 1.5f);
        }
    }

    void ReiniciarAtaque()
    {
        estaAtacando = false;
        agente.speed = speed;
        agente.angularSpeed = angularSpeed;
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bala")
        {
            
            Destroy(gameObject);
        }
    }*/

}
