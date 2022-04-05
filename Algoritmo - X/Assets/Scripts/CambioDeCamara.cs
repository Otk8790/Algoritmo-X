using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioDeCamara : MonoBehaviour
{
    public Transform FirstPerson, ThirtPerson;
    public bool vista;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            vista = true;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            vista = false;
        }
        if (vista == true)
        {
            transform.position = Vector3.Lerp(transform.position, FirstPerson.position, 8 * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, ThirtPerson.position, 8 * Time.deltaTime);
        }
    }
}
