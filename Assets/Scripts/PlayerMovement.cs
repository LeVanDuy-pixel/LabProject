using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    GameController controller;
    
    private Rigidbody2D rb;
    private Vector2 moveDirector;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.score > 20)
        {
            moveSpeed = 8.5f;
            if(controller.score > 40)
            {
                moveSpeed = 10;
            }
        }
        ProcessInputs();
    }
    void FixedUpdate()
    {
        Move();
    }
    public void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirector = new Vector2(moveX, moveY).normalized;
    }
    private void Move()
    {
        rb.velocity = new Vector2(moveDirector.x * moveSpeed, moveDirector.y * moveSpeed);

    }
}
