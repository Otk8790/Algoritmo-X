using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWhitFloat : MonoBehaviour
{
    CharacterController Player;
    Vector3 groundPosition;
    Vector3 lastGroundPosition;
    string groundName;
    string lastGroundName;


    // Start is called before the first frame update
    void Start()
    {
        Player = this.GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Player.isGrounded)
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, Player.height / 40f, -transform.up, out hit))
            {
                GameObject groundedIn = hit.collider.gameObject;
                groundName = groundedIn.name;
                groundPosition = groundedIn.transform.position;

                if (groundPosition != lastGroundPosition && groundName == lastGroundName)
                {
                    this.transform.position += groundPosition - lastGroundPosition;
                }

                lastGroundName = groundName;
                lastGroundPosition = groundPosition;

            }
        }
        else if (!Player.isGrounded)
        {
            lastGroundName = null;
            lastGroundPosition = Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Player = this.GetComponent<CharacterController>();

        Gizmos.DrawWireSphere(transform.position, Player.height / 40f);

    }
}
