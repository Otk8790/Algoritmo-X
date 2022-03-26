﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimatorController;
    public CharacterController player;

    [Header("MOVIMIENTO")]
    private float horizontalMove;
    private float verticalMove;
    private Vector3 playerInput;
    public float playerSpeed;

    [Header("SALTO")]
    public float gravity = 9.8f;
    public float fallVeclocity;
    public float jumpForce;

    [Header("CAMARA/DIRECCION")]
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 movePlayer;

    public bool isOnSlope = false;
    private Vector3 hitNormal;
    public float slideVelocity;
    public float slopeForceDown;
    //public AudioSource salto;


    [Header("ESCUDO")]
    private GameObject _shieldGameObject;[SerializeField]
    public bool shieldsActive = false;
    private PlayerAnimation _playerAnim;
    private bool puedoSaltar;

    [Header("PARTICULAS")]
    [SerializeField] private ParticleSystem polvoPies;
    private ParticleSystem.EmissionModule emisionPolvoPies;

    //Variables animacion
    //public Animator playerAnimatorController;

    // Start is called before the first frame update
    void Start()
    {
        puedoSaltar = false;
        player = GetComponent<CharacterController>();
        playerAnimatorController = GetComponent<Animator>();
        _playerAnim = GetComponent<PlayerAnimation>();
        emisionPolvoPies = polvoPies.emission;
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

<<<<<<< HEAD
        checkPolvoPies();
=======
        Ataque();
>>>>>>> c9db534d361e05a73a63ba4aa576d14f3a837023

        player.Move(movePlayer * Time.deltaTime);
    }
    public void movimiento()
    {
        if (shieldsActive == false)
        {
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
        if (player.isGrounded && Input.GetButtonDown("Jump") && shieldsActive == false)
        {
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
        if (Input.GetKeyDown(KeyCode.F) && player.isGrounded)
        {
            playerAnimatorController.SetTrigger("Attack");
        }
    }

    private void Escudo()
    {
        if (Input.GetKeyDown(KeyCode.Z) && player.isGrounded)
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
    private void checkPolvoPies()
    {
        if (puedoSaltar == true && horizontalMove != 0)
        {
            emisionPolvoPies.rateOverTime = 50;
        }
        else
        {
            emisionPolvoPies.rateOverTime = 0;
        }
    }

    private void OnAnimatorMove()
    {
        
    }
    public void EstoyCayendo()
    {
        playerAnimatorController.SetBool("IsGrounded", false);
        _playerAnim.Jump(false);
    }
}
