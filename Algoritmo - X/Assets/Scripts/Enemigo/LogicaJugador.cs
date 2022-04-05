using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaJugador : MonoBehaviour {
    public Vida vida;
    public bool Vida0 = false;
    private Animator anim;

	// Use this for initialization
	void Start () {
        vida = GetComponent<Vida>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        RevisarVida();
	}

    void RevisarVida()
    {
        if (Vida0) return;
        if(vida.valor <= 0)
        {
            Vida0 = true;
            anim.SetTrigger("morir");
            Invoke("ReiniciarJuego", 5f);
        }
    }

    void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   
}
