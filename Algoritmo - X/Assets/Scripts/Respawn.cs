using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private Transform pos3;
    [SerializeField] private Transform player;
    private int checkPointCount = 1;

    private void OnTriggerEnter(Collider other)
    {
        CharacterController CC = player.GetComponent<CharacterController>();
        if (other.gameObject.tag == "checkpoint")
        {
            BoxCollider BC = other.gameObject.GetComponent<BoxCollider>();
            BC.enabled = false;
            checkPointCount++;
        }
        if (other.gameObject.tag == "Respawn")
        {
            if (checkPointCount == 2)
            {
                CC.enabled = false;
                player.transform.position = pos2.transform.position;
                CC.enabled = true;
            }
            else if (checkPointCount == 3)
            {
                CC.enabled = false;
                player.transform.position = pos3.transform.position;
                CC.enabled = true;
            }
            else
            {
                CC.enabled = false;
                player.transform.position = pos1.transform.position;
                CC.enabled = true;
            }
        }
    }
}
