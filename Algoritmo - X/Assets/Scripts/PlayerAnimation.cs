using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    public void Jump(bool jumping)
    {
        _anim.SetBool("Jumping", jumping);
    }

    public void Escudo(bool escudo)
    {
        _anim.SetBool("Escudo", escudo);
    }

    public void Attack(bool attack)
    {
        _anim.SetBool("Attack", attack);
    }
}
