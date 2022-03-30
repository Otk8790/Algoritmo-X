using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    Animator animator;

    int cantidad_click;
    bool puedo_dar_click;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cantidad_click = 0;
        puedo_dar_click = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) { Iniciar_combo(); }
    }

    void Iniciar_combo()
    {
        if(puedo_dar_click)
        {
            cantidad_click++;
        }

        if(cantidad_click == 1)
        {
            animator.SetInteger("Ataque", 1);
        }
    }

    public void Verificar_combo()
    {
        puedo_dar_click = false;

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Combo") && cantidad_click == 1)
        {
            animator.SetInteger("Ataque", 0);
            puedo_dar_click = true;
            cantidad_click = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Combo") && cantidad_click >= 2)
        {
            animator.SetInteger("Ataque", 2);
            puedo_dar_click = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Combo2") && cantidad_click == 2)
        {
            animator.SetInteger("Ataque", 0);
            puedo_dar_click = true;
            cantidad_click = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Combo2") && cantidad_click >= 3)
        {
            animator.SetInteger("Ataque", 3);
            puedo_dar_click = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Combo3"))
        {
            animator.SetInteger("Ataque", 0);
            puedo_dar_click = true;
            cantidad_click = 0;
        }
    }
}
