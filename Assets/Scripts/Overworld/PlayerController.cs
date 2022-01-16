using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed;
    public float jumpForce;

    private Vector2 moveInput;

    public LayerMask whatIsGround;
    public Transform groundPoint;
    private bool isGrounded;

    private bool isInteracting;

    public Animator anim;
    public SpriteRenderer sr;
    private bool movingBackwards;
    public Animator flipAnim;

    // Update is called once per frame
    void Update()
    {
        if (!isInteracting)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();

            rb.velocity = new Vector3(moveInput.x * moveSpeed, rb.velocity.y, moveInput.y * moveSpeed);

            anim.SetFloat("moveSpeed", rb.velocity.magnitude);

            RaycastHit hit;
            if (Physics.Raycast(groundPoint.position, Vector3.down, out hit, .3f, whatIsGround))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity += new Vector3(0f, jumpForce, 0f);
            }

            anim.SetBool("onGround", isGrounded);

            if (!sr.flipX && moveInput.x < 0)
            {
                sr.flipX = true;
                flipAnim.SetTrigger("Flip");
            }
            else if (sr.flipX && moveInput.x > 0)
            {
                sr.flipX = false;
                flipAnim.SetTrigger("Flip");
            }

            if (!movingBackwards && moveInput.y > 0)
            {
                movingBackwards = true;
            }
            else if (movingBackwards && moveInput.y < 0)
            {
                movingBackwards = false;
            }

            anim.SetBool("movingBackwards", movingBackwards);
        }
    }

    public void ToggleInteraction()
    {
        isInteracting = !isInteracting;
    }
}