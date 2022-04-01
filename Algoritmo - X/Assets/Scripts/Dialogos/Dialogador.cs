using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogador : MonoBehaviour
{
    public int estadoActual = 0;
    public EstadoDialogo[] estados;
    public Collider ocultar;
    public GameObject dialogueMark;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogueMark.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            dialogueMark.SetActive(true);
            if(Input.GetKeyDown(ControlDialogos.singleton.configuracion.teclaInicioDialogo))
            {
                StartCoroutine(ControlDialogos.singleton.Decir(estados[estadoActual].frases));
                StartCoroutine(DesactivarDialogo());
            }
        }
    }
    IEnumerator DesactivarDialogo()
    {
        yield return new WaitForSeconds(3.0f);
        ocultar.enabled = false;
        dialogueMark.SetActive(false);
    }
}
