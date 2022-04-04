using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        if (other.tag == "Terreno")
        {
            Destroy(gameObject);
        }
    }
}
