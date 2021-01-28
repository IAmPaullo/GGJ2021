using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk : MonoBehaviour
{
    public float moveSpeed = 5;
    public float mvLimiter = 0.7f;
    public Rigidbody2D rb;
    Vector2 movement;
    Animator animator;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //PlayAnim();
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);


    }


    private void FixedUpdate()
    {
        if(movement.x != 0 && movement.y != 0)
        {
            movement.x *= mvLimiter;
            movement.y *= mvLimiter;
        }
        
        
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

    }

    void PlayAnim()
    {
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Input.GetAxisRaw("Horizontal") <= 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

}
