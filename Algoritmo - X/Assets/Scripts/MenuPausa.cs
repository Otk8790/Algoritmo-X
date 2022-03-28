using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public TerceraPersona terceraPersona;
    public GameObject menuPausa;

    public int numeroEscena;

    public static bool juegoPausado;

    void Start()
    {
        menuPausa.SetActive(false);
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
        
        if (juegoPausado == true)
        {
            Renudar();
            terceraPersona.enabled = true;
        }
        else
        {
            Pausa();
            terceraPersona.enabled = false;
        }
        }
    }
    public void Pausa()
    {
        Time.timeScale = 0f;
        menuPausa.SetActive(true);
        juegoPausado = true;
    }
    public void Renudar()
    {
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
        juegoPausado = false;

    }
    public void MenuPrincipal()
    {
        SceneManager.LoadScene(numeroEscena);
    }

    public void Cerrar()
    {
        Application.Quit();
    }
}
