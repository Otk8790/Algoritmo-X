using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlDialogosLucian : MonoBehaviour
{
    // Singleton
    public static ControlDialogosLucian singleton;
    public static bool enDialogo;
    public GameObject dialogo;
    
    public TextMeshProUGUI txtDialogo;
    /* public Image imCara; */

    [Header("Config del teclado")]
    
    public ConfigDialogos configuracion;

    [Header("Palabras Lucian")]
    public FraseLucian[] dialogoLucian;

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
        Prueba();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Decir(FraseLucian[] _dialogo)
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
        StartCoroutine(Decir(dialogoLucian));
    }
}

[System.Serializable]
public class FraseLucian
{
    public string texto;
}
[System.Serializable]
public class EstadoDialogoLucian
{
    public Frase[] frases;
}


