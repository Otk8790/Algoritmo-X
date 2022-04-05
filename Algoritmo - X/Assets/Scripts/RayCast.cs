using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCast : MonoBehaviour
{
    // Start is called before the first frame update
    public int rango;
    public GameObject camara;
    public Image IndicadorCentral;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(camara.transform.position, camara.transform.forward, out hit, rango))
        {
            IndicadorCentral.color = Color.white;

            if(hit.collider.GetComponent<Inventory>()== true)
            {
                IndicadorCentral.color = Color.red;

                if(Input.GetKeyDown(KeyCode.E))
                {
                    /* hit.collider.GetComponent<Inventory>().AddItem = true; */
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(camara.transform.position, camara.transform.forward * rango);
    }
}
