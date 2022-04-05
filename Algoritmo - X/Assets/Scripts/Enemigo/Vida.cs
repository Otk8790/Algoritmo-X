using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour {
    public float valor = 100;
    private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RecibirDaño(float daño)
    {
        anim.SetTrigger("daño");
        valor -= daño;
        if(valor < 0)
        {
            valor = 0;
        }
    }
}
