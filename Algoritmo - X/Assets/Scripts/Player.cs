using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;
    private Animator anim;
    public float x, y;

    public Rigidbody rb;
    public float fuerzaDeSalto = 5f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        Salto();
    }

    private void Movimiento()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);

        anim.SetFloat("Speed", y);
    }

    private void Salto()
    {
        if(Input.GetKeyDown (KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, fuerzaDeSalto, 0), ForceMode.Impulse);
        }
    }
}
