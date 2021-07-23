 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool jumpKeyPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    //  private bool isGrounded;
    // [SerializeField] private Transform groundCheckTransform = null; -> proper way
    public Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    private int superJumps;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
            
    }

    private void FixedUpdate() //reliable phys, keeps in sync w proper phys
    {
        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            //checksphere
            return;
        }
        //if (!isGrounded)
        //{
        //    return;
        //}

        if (jumpKeyPressed)
        {
            float jumpPower = 5;
            if (superJumps > 0)
            {
                jumpPower *= 2;
                superJumps--;
            }
            var component = rigidbodyComponent;
            component.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);

            jumpKeyPressed = false;
                
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Destroy(other.gameObject);
            superJumps++;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    isGrounded = true;
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    isGrounded = false;
    //}
}

