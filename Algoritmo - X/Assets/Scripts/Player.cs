using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;
    private PlayerAnimation _playerAnim;
    private ShieldAnimations _shieldAnim;
    private Animator anim;
    public float x, y;

    public Rigidbody rb;
    public float fuerzaDeSalto = 5f;
    /* public bool playerShield = false; */
    [SerializeField]
    private GameObject _shieldGameObject;
    public bool shieldsActive = false;
    public bool puedoSaltar;
    public float modificadorGravedad = 2;
    public bool puede_moverse = true;

    [Header("PARTICULAS")]
    [SerializeField] private ParticleSystem polvoPies;
    [SerializeField] private ParticleSystem polvoSalto;

    private ParticleSystem.EmissionModule emisionPolvoPies;

    void Start()
    {
        puedoSaltar = false;
        anim = GetComponent<Animator>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _shieldAnim = GetComponent<ShieldAnimations>();
        Physics.gravity *= modificadorGravedad;
        emisionPolvoPies = polvoPies.emission;
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        Salto();
        Escudo();
        checkPolvoPies();
    }

    private void checkPolvoPies()
    {
        if(puedoSaltar == true && x != 0)
        {
            emisionPolvoPies.rateOverTime = 50;
        }
        else
        {
            emisionPolvoPies.rateOverTime = 0;
        }
    }

    private void Escudo()
    {
        if(Input.GetKeyDown(KeyCode.Z) && puedoSaltar)
        {
            puede_moverse = false;
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
        puede_moverse = true;
    }

    private void Movimiento()
    {
        if(puede_moverse){
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");

            transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
            transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);

            anim.SetFloat("Speed", y);
        }

        /*horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        anim.SetFloat("Speed", playerInput.magnitude * playerSpeed);*/



    }

    private void Salto()
    {
        if(puedoSaltar)
        {
            if(Input.GetKeyDown (KeyCode.Space) && puede_moverse)
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
