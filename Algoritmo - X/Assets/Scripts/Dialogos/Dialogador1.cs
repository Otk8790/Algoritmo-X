using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogador1 : MonoBehaviour
{
    public int estadoActual = 0;
    public EstadoDialogo[] estados;
    public Collider ocultar;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inciarDialogo();
        }
    }
    
    public void inciarDialogo()
    {
        StartCoroutine(ControlDialogos.singleton.Decir(estados[estadoActual].frases));
        StartCoroutine(DesactivarDialogo());
    }
    IEnumerator DesactivarDialogo()
    {
        yield return new WaitForSeconds(3.0f);
        ocultar.enabled = false;
    }
}
