using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;
    private PlayerAnimation _playerAnim;
    private Animator anim;
    public float x, y;

    public Rigidbody rb;
    public float fuerzaDeSalto = 5f;
    /* public bool playerShield = false; */
    [SerializeField]
    private GameObject _shieldGameObject;
    public bool shieldsActive = false;
    public bool puedoSaltar;
    // Start is called before the first frame update
    void Start()
    {
        puedoSaltar = false;
        anim = GetComponent<Animator>();
        _playerAnim = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        Salto();
        Escudo();
    }

    private void Escudo()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            shieldsActive = true;
            _shieldGameObject.SetActive(true);
            _playerAnim.Escudo(true);
            StartCoroutine(DesactivateShields());
        }
    }

    IEnumerator DesactivateShields()
    {
        yield return new WaitForSeconds(5.0f);
        _shieldGameObject.SetActive(false);
        shieldsActive = false;
        _playerAnim.Escudo(false);
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

        if(puedoSaltar)
        {
            if(Input.GetKeyDown (KeyCode.Space))
            {
                _playerAnim.Jump(true);
                rb.AddForce(new Vector3(0, fuerzaDeSalto, 0), ForceMode.Impulse);
            }
            anim.SetBool("tocoSuelo", true);
        }
        else
        {
            EstoyCayendo();
        }
    
    }

    public void EstoyCayendo()
    {
        anim.SetBool("tocoSuelo", false);
        _playerAnim.Jump(false);
    }
}
