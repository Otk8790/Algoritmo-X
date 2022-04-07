using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform1 : MonoBehaviour
{
    public GameObject Ledge;
    public GameObject Player;

    private void OnTriggerEnter()
    {
        Player.transform.parent = Ledge.transform;
    }
}
