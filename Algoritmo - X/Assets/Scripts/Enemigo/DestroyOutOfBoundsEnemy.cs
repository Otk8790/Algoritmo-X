using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBoundsEnemy : MonoBehaviour
{
    private float topBound = 23f;
    private float lowerBound = -23f;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        /* GameObject playerControllerObject = GameObject.Find("Player");
        playerController = playerControllerObject.GetComponent<PlayerController>(); */
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x > topBound){
            Destroy(gameObject);
        }

        if(this.transform.position.x < lowerBound){
            //Debug.Log("GAME OVER!!!");
            Destroy(gameObject);
        }
    }
}
