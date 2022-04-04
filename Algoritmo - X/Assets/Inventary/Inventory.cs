using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //public PlayerController playerController;
    public TerceraPersona terceraPersona;
    [Header("Referencia Slots")]
    private bool inventoryEnable;
    public GameObject inventory;
    private int allSlots;
    [Header("Funcionamiento Slots")]
    private int enableSlots;

    private GameObject[] slot;

    public GameObject slotHolder;

    void Start()
    {
        allSlots = slotHolder.transform.childCount;

        slot = new GameObject[allSlots];

        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;

            if (slot[i].GetComponent<Slots>().item == null)
            {
                slot[i].GetComponent<Slots>().emty = true;
            }
        } 
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventory.gameObject.activeSelf)
            {
                inventory.gameObject.SetActive(false);
                Time.timeScale = 1f;
                terceraPersona.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                inventory.gameObject.SetActive(true);
                Time.timeScale = 0f;
                terceraPersona.enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item")
        {
            GameObject itemPickedUp = other.gameObject;

            Item item = itemPickedUp.GetComponent<Item>();

            AddItem(itemPickedUp, item.ID, item.type, item.descripcion, item.icon);
        }
    }

    public void AddItem(GameObject itemObject, int itemID, string itemType, string itemDescripcion, Sprite itemIcon)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if (slot[i].GetComponent<Slots>().emty)
            {
                itemObject.GetComponent<Item>().pickedUp = true;

                slot[i].GetComponent<Slots>().item = itemObject;
                slot[i].GetComponent<Slots>().ID = itemID;

                slot[i].GetComponent<Slots>().type = itemType;
                slot[i].GetComponent<Slots>().descripcion = itemDescripcion;
                slot[i].GetComponent<Slots>().icon = itemIcon;

                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive(false);

                slot[i].GetComponent<Slots>().UpdateSlop();


                slot[i].GetComponent<Slots>().emty = false;
            }
            return;
        }
    }
}
