using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slots : MonoBehaviour, IPointerClickHandler 
{
    public GameObject item;
    public int ID;
    public string type;
    public string descripcion;

    public bool emty;
    public Sprite icon;

    public Transform slotIconGameObject;
    private void Start()
    {
        slotIconGameObject = transform.GetChild(0);
    }

    public void UpdateSlop()
    {
        slotIconGameObject.GetComponent<Image>().sprite = icon;
    }
    public void UseItem()
    {
        item.GetComponent<Item>().ItemUsage();
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        UseItem();
    }
}
