using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ConfigDialogos : ScriptableObject
{
    [Header("General")]
    public float tiempoLetra = 0.1f;
    /* public PersonajeDialogo[] personajes; */
    [Header("Teclas")]
    public KeyCode teclaSkip = KeyCode.Space;

    public KeyCode teclaSiguienteFrase;
    public KeyCode teclaInicioDialogo = KeyCode.B;


}
