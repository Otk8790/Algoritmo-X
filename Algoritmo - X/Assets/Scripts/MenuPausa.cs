using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField]
    private GameObject botonPausa;
    [SerializeField]
    private GameObject menuPausa;

    public int numeroEscena;

    void Start()
    {
        this.gameObject.SetActive(true);
    }
    public void Pausa()
    {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }
    public void Renudar()
    {
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }
    public void MenuPrincipal()
    {
        SceneManager.LoadScene(numeroEscena);
    }
}
