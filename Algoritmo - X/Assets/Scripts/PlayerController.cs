using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private float distanceToGround;
    public float speed = 10f;
    public float jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovimiento();
        UpdateJump();
    }

    private void UpdateMovimiento()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 velocity;

        if(hor != 0.0f || ver != 0.0f){
            velocity = (transform.forward * ver + transform.right * hor).normalized * speed;
            /* rigidbody.MovePosition(transform.position + dir * speed * Time.deltaTime); */
        } else {
            velocity = Vector3.zero;
        }

        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;
    }

    private void UpdateJump()
    {
        if(Input.GetButtonDown("Jump") && IsGrounded()) {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics.BoxCast(transform.position, new Vector3(0.4f, 0f, 0.4f), Vector3.down, Quaternion.identity, distanceToGround + 0.1f);
    }
    
}
