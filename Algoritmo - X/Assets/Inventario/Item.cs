using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Identificacion")]
    public int ID;
    public string type;
    [Header("Identificacion/Icon")]
    public string descripcion;
    public Sprite icon;

    [HideInInspector]
    public bool pickedUp;

}
