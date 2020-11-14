using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float movespeed;
    public float sprintspeed;
    private float speed;
    public Rigidbody2D rb;
    private Vector2 md;

    void Awake() {
        speed = movespeed;
    }
    
    void Update()
    {
        ProcessInputs();

        if(Input.GetButtonDown("Shift"))
        {
            speed = sprintspeed;
        }
        if(Input.GetButtonUp("Shift"))
        {
            speed = movespeed;
        }
    }
    
    void FixedUpdate()
    {
        Move();
    }
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        md = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2 (md.x * speed, md.y * speed);
    }

}
