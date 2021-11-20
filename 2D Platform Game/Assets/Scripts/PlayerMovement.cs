using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed;
    private float moveInput;
    public float jumpForce;

    private float jumpTimeCounter; //tiempo que lleva saltando
    public float jumpTime; //Tiempo que puede saltar en total
    private bool isJumping;

    private bool isGrounded; //Comprueba que esta en el suelo
    public Transform feetPos; //Posicion de los pies del personaje
    public float checkRadius;
    public LayerMask whatIsGround; //que es tierra para realizar el salto

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround); //comprueba si está sobre suelo

        //Mover el sprite del jugador
        if(moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }else if (moveInput <0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if(jumpTimeCounter > 0) //seguira saltando hasta que el contador llegue a 0
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }
}
