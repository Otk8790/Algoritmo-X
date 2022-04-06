using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataforma : MonoBehaviour
{
    public Transform[] target;
    public float speed = 6.0f;

    int curPos = 0;
    int nextPos = 1;

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target[nextPos].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target[nextPos].position) <= 0)
        {
            curPos = nextPos;
            nextPos++;

            if (nextPos > target.Length - 1)
                nextPos = 0;
        }
    }
}
