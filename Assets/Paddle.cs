using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    float speed = 6f;
    Vector2 moveVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        processInput();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
    }

    void processInput()
    {
        float inputHori = Input.GetAxisRaw("Horizontal");
        Vector2 moveInput = new Vector2(inputHori, 0);
        moveVelocity = moveInput.normalized * speed;
    }
}
