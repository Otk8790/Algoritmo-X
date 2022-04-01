using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlDialogos : MonoBehaviour
{
    // Singleton
    public static ControlDialogos singleton;
    public static bool enDialogo;
    public GameObject dialogo;
    
    public TextMeshProUGUI txtDialogo;
    /* public Image imCara; */

    [Header("Config del teclado")]
    
    public ConfigDialogos configuracion;

    [Header("Ensayos")]
    public Frase[] dialogoEnsayo;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Decir(Frase[] _dialogo)
    {
        dialogo.SetActive(true);
        enDialogo = true;
        for (int i = 0; i < _dialogo.Length; i++)
        {
            txtDialogo.text = "";
            for(int j = 0; j < _dialogo[i].texto.Length + 1; j++)
            {
                yield return new WaitForSeconds(configuracion.tiempoLetra);
                if(Input.GetKey(configuracion.teclaSkip))
                {
                    j = _dialogo[i].texto.Length;
                    
                }
                txtDialogo.text = _dialogo[i].texto.Substring(0,j);
            }
            txtDialogo.text = _dialogo[i].texto;
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => Input.GetKeyUp(configuracion.teclaSiguienteFrase));
        }
        dialogo.SetActive(false);
        enDialogo = false;
    }
    /* [ContextMenu("Activar prueba")] */
    public void Prueba()
    {
        StartCoroutine(Decir(dialogoEnsayo));
    }
}

[System.Serializable]
public class Frase 
{
    public string texto;
    public int personaje;
}
[System.Serializable]
public class EstadoDialogo
{
    public Frase[] frases;
}

/* [System.Serializable]
public class CaraDialogo
{
    public string letra;
    public Sprite cara;
}

[System.Serializable]
public class PersonajeDialogo
{
    public CaraDialogo[] caras;
    public Sprite GetCara(string l)
    {
        int indice = 0;
        for (int i = 0; i < caras.Length; i++)
        {
            if(caras[i].letra == l)
            {
                indice = i;
                break;
            }
        }
        return caras[indice].cara;

    }
    public Sprite GetCara(int i)
    {
        i = Mathf.Clamp(i, 0, caras.Length - 1);
        return(caras[i].cara);
    }
} */

