using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAnimations : MonoBehaviour
{
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    
    public void Shield(bool shield)
    {
        _anim.SetBool("Shields_Active", shield);
    }
}
