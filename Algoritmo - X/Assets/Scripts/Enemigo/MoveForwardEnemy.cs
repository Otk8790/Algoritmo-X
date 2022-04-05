using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardEnemy : MonoBehaviour
{
    public float speed = 40f;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right*speed*Time.deltaTime);
    }
}
