using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Item item;
    public static PlayerController instance;
    private DronEnemy dronEnemy;
    //MovimientoPersonaje
    private float horizontalMove;
    private float verticalMove;

    private Vector3 playerInput;

    public float playerSpeed;
    public float gravity = 9.8f;
    public float fallVeclocity;
    public float jumpForce;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 movePlayer;

    public bool isOnSlope = false;
    private Vector3 hitNormal;
    public float slideVelocity;
    public float slopeForceDown;
    //public AudioSource salto;

    public Animator playerAnimatorController;

    public CharacterController player;

    [SerializeField]
    private GameObject _shieldGameObject;
    private PlayerAnimation _playerAnim;

    [Header("CONDICIONES")]
    public bool shieldsActive = false;
    public bool puedeMoverse;
    public bool puedeAtacar;
    public bool activarEscudo;


    [Header("TiEMPO")]
    public float tiempoDeAtaque = 1.5f;
    private float timeAtaque = 0f;
    public float tiempoDeEscudo = 12.0f;
    private float timeEscudo = 0f;

    [Header("VIDA")]
    [SerializeField]
    /* int vidaMax = 5;
    int vidaActual;
    public Image mascaradeDaño;
    public Image barraverde;
    public float valorAlfa; */

    public float vida = 100;
    public Image barraDeVida;
    public TextMeshProUGUI VidaPor;


    [Header("PARTICULAS")]
    [SerializeField] private ParticleSystem polvoPies;

    /* [Header("CAMARA SHAKE")]
    [SerializeField] private CameraShake cameraShake; */

    [Header("DISPARO")]
    public Transform spawnPoint;
    public GameObject bullet;
    public float shotForce = 1500f;
    //Tiempo
    public float shotRate = 4.0f;
    private float shotRateTime = 0f;
    public Transform puntoDeDisparo;
    public float daño = 20f;
    public bool tieneArma;

    [Header("SONIDO")]

    public AudioSource DisparoSound;
    public GameObject SonidoDisparo;

    public AudioSource SaltoSound;
    public GameObject SonidoSalto;
    public AudioSource MorirSound;
    public GameObject SonidoMorir;
    public AudioSource GolpeSound;
    public GameObject SonidoGolpe;


    /* private ParticleSystem.EmissionModule emisionPolvoPies; */

    //Variables animacion
    //public Animator playerAnimatorController;


    void awake()
    {
        instance = this;
    }
    void Start()
    {
        tieneArma = false;
        puedeAtacar = true;
        activarEscudo = true;
        puedeMoverse = true;

        player = GetComponent<CharacterController>();
        playerAnimatorController = GetComponent<Animator>();
        _playerAnim = GetComponent<PlayerAnimation>();
        setVidaPor();
        /* vidaActual = vidaMax; */
    }

    // Update is called once per frame
    void Update()
    {
        movimiento();

        playerAnimatorController.SetFloat("PlayerWalkVelocity", playerInput.magnitude * playerSpeed);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();

        PlayerSkills();

        Escudo();

        Ataque();

        Disparo();

        player.Move(movePlayer * Time.deltaTime);

        vida = Mathf.Clamp(vida, 0, 100);

        barraDeVida.fillAmount = vida / 100;
    }
    public void movimiento()
    {
        if (ControlDialogos.enDialogo || ControlDialogosLucian.enDialogo)
            return;
        if (puedeMoverse)
        {
            polvoPies.Play();
            horizontalMove = Input.GetAxis("Horizontal");
            verticalMove = Input.GetAxis("Vertical");

            playerInput = new Vector3(horizontalMove, 0, verticalMove);
            playerInput = Vector3.ClampMagnitude(playerInput, 1);
        }
    }
    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    public void PlayerSkills()
    {
        if (ControlDialogos.enDialogo || ControlDialogosLucian.enDialogo)
            return;
        if (player.isGrounded && Input.GetButtonDown("Jump") && shieldsActive == false)
        {
            //Instantiate(caida);
            
            Instantiate(SonidoSalto);
            GameObject sonidoSalto;
            sonidoSalto = Instantiate(SonidoSalto);
            Destroy(sonidoSalto, 2);

            polvoPies.Stop();
            fallVeclocity = jumpForce;
            movePlayer.y = fallVeclocity;
            playerAnimatorController.SetTrigger("PlayerJump");

        }
    }

    void SetGravity()
    {
        if (player.isGrounded)
        {
            fallVeclocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVeclocity;
        }
        else
        {
            fallVeclocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVeclocity;
            playerAnimatorController.SetFloat("PlayerVerticalVelocity", player.velocity.y);
        }

        playerAnimatorController.SetBool("IsGrounded", player.isGrounded);
    }

    private void Ataque()
    {
        if (Input.GetButtonDown("Fire1") && player.isGrounded && puedeAtacar)
        {
            if (ControlDialogos.enDialogo || ControlDialogosLucian.enDialogo)
                return;
            if (Time.time > timeAtaque)
            {
                playerAnimatorController.SetTrigger("Attack");
                timeAtaque = Time.time + tiempoDeAtaque;
            }
        }
    }
    public void EmpiezaDisparo()
    {
        puedeMoverse = false;
        puedeAtacar = true;
        activarEscudo = false;
    }

    private void Disparo()
    {
        if (Input.GetButtonDown("Fire2") && player.isGrounded && puedeAtacar)
        {
            if (ControlDialogos.enDialogo || ControlDialogosLucian.enDialogo)
                return;
            if (Time.time > shotRateTime && tieneArma == false)
            {
                playerAnimatorController.SetTrigger("Disparar");
                GameObject newBullet;
                newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);

                shotRateTime = Time.time + shotRate;
                Destroy(newBullet, 2);

                GameObject sonidoDisparo;
                sonidoDisparo = Instantiate(SonidoDisparo);
                Destroy(sonidoDisparo, 2);
            }
        }

    }

    public void TerminaDisparo()
    {
        puedeMoverse = true;
        puedeAtacar = true;
        activarEscudo = true;
    }

    private void Escudo()
    {
        if (ControlDialogos.enDialogo || ControlDialogosLucian.enDialogo)
            return;
        if (Input.GetKeyDown(KeyCode.Z) && player.isGrounded)
        {
            if (activarEscudo && Time.time > timeEscudo)
            {
                shieldsActive = true;
                _shieldGameObject.SetActive(true);
                _playerAnim.Escudo(true);
                StartCoroutine(DesactivateShields());
                puedeMoverse = false;
                timeEscudo = Time.time + tiempoDeEscudo;
            }
        }
    }

    IEnumerator DesactivateShields()
    {
        yield return new WaitForSeconds(5.0f);
        _shieldGameObject.SetActive(false);
        shieldsActive = false;
        _playerAnim.Escudo(false);
        puedeMoverse = true;

    }

    public void quitarVida()
    {
        vida -= 20;
        setVidaPor();

        Instantiate(SonidoGolpe);
        GameObject sonidoGolpe;
        sonidoGolpe = Instantiate(SonidoGolpe);
        Destroy(sonidoGolpe, 2);
    }

    public void dañozombi()
    {
        vida -= 30;
        setVidaPor();
        playerAnimatorController.SetTrigger("daño");

        if(vida <= 0)
        {
            playerAnimatorController.SetTrigger("morir");
            Debug.Log("muerto");
        }
    }

    public void setVidaPor()
    {
        VidaPor.text = vida.ToString() + "%" ;
    }

    private void OnTriggerEnter(Collider other){
        
        //Si el objeto con el que el animal colisiona tiene el Tag Projectile

        if(other.CompareTag("Bala")){
            //Destrute el cohete
            Destroy(other.gameObject);
            quitarVida();
            playerAnimatorController.SetTrigger("daño");

            if(vida <= 0){
                playerAnimatorController.SetTrigger("morir");
                Instantiate(SonidoMorir);
                GameObject sonidoMorir;
                sonidoMorir = Instantiate(SonidoMorir);
                Destroy(sonidoMorir, 2);

                Debug.Log("Game Over");
            }       
        }
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
           
            other.gameObject.SetActive(false);
            // reducir la vida
            vidaActual -= 1;
            /* StartCoroutine(cameraShake.Shake()); */
    /* SacudirCamara(.5f); */
    /*valorAlfa = 1 / (float)vidaMax * (vidaMax - vidaActual);
    mascaradeDaño.color = new Color(1, 1, 1, valorAlfa);
    //vida.text = vidaActual.ToString();
    barraverde.fillAmount = (float)vidaActual / vidaMax;
} 
}*/
}
