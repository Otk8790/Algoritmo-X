using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("MOVIMIENTO")]
    private float horizontalMove;
    private float verticalMove;

    private Vector3 playerInput;

    [Header("VELOCIDAD")]
    public float playerSpeed;
    public float gravity = 9.8f;
    public float fallVeclocity;
    public float jumpForce;

    [Header("CAMARA")]
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
    public bool activarEscudo;
    public float tiempoDeAtaque = 1.5f;
    private float timeAtaque = 0;
    public float tiempoDeEscudo = 5.0f;
    private float timeEscudo = 0;

    [Header("PARTICULAS")]
    //[SerializeField] private ParticleSystem polvoPies;
    /* private ParticleSystem.EmissionModule emisionPolvoPies; */

    //Variables animacion
    //public Animator playerAnimatorController;

    [Header("VIDA")]
    int vidaMax = 5;
    int vidaActual;
    public Image mascaradeDaño;
    public TextMeshProUGUI vida;
    public Image barraverde;
    public float valorAlfa;
    public Transform camaraAsacudir;
    float magnitudSacudida;

    // Start is called before the first frame update
    void Start()
    {
        activarEscudo = true;
        puedeMoverse = true;
        
        player = GetComponent<CharacterController>();
        playerAnimatorController = GetComponent<Animator>();
        _playerAnim = GetComponent<PlayerAnimation>();
        vidaActual = vidaMax;
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

        /* checkPolvoPies(); */
        Ataque();

        Disparo();

        player.Move(movePlayer * Time.deltaTime);
    }
    public void movimiento()
    {
        //InventarioCerrado();
        if (shieldsActive == false && puedeMoverse == true)
        {
            horizontalMove = Input.GetAxis("Horizontal");
            verticalMove = Input.GetAxis("Vertical");

            playerInput = new Vector3(horizontalMove, 0, verticalMove);
            playerInput = Vector3.ClampMagnitude(playerInput, 1);
            //polvoPies.Play();
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
        if (player.isGrounded && Input.GetButtonDown("Jump") && shieldsActive == false)
        {
            //Instantiate(caida);
            //polvoPies.Stop();
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
        if (Input.GetButtonDown("Fire1") && player.isGrounded)
        {
            if (Time.time>timeAtaque)
            {
                playerAnimatorController.SetTrigger("Attack");

                timeAtaque = Time.time + tiempoDeAtaque;

            }
        }
    }

    private void Disparo()
    {
        if (Input.GetButtonDown("Fire2") && player.isGrounded)
        {
            playerAnimatorController.SetTrigger("Disparar");
        }
    }

    private void Escudo()
    {
        if (Input.GetKeyDown(KeyCode.Z) && player.isGrounded)
        {
            if (activarEscudo == true && Time.time>timeEscudo)
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
    /* private void checkPolvoPies()
    {
        if (puedoSaltar == true && horizontalMove != 0)
        {
            emisionPolvoPies.rateOverTime = 50;
        }
        else
        {
            emisionPolvoPies.rateOverTime = 0;
        }
    } */

    private void OnAnimatorMove()
    {
        
    }
    public void EstoyCayendo()
    {
        playerAnimatorController.SetBool("IsGrounded", false);
        _playerAnim.Jump(false);
    }

     private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
            // reducir la vida
            vidaActual -= 1;
            /* SacudirCamara(.5f); */
            valorAlfa = 1 / (float)vidaMax * (vidaMax - vidaActual);
            mascaradeDaño.color = new Color(1, 1, 1, valorAlfa);
            //vida.text = vidaActual.ToString();
        }
     }
}
