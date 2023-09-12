using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 13f;
    public float speedMultiplier = 1;
    Vector3 velocity;
    public float gravity = -19.6f;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 2f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime * speedMultiplier);

        if(isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


    }



   
}
