using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Varaibles
    public float speed = 3f;
    public float jumpForce = 3f;

    //Referencias
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    //Movimiento 
    private Vector2 _movement;
    private bool _facingRight = true;
    private bool _isGrounded;
    private bool _onWall;
    
    private void Awake() //Se toman los componentes necesarios
                         
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }


    
    void Update()
    {
        //Registar el Movimiento 
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _movement = new Vector2 (horizontalInput, 0);

        //Hacer que el personaje gire
        if (horizontalInput < 0f && _facingRight == true)
        {
            Flip();
        }
        else if (horizontalInput > 0f && _facingRight == false)
        {
            Flip();
        }

        //Salto
        if (Input.GetButtonDown("Jump") && _isGrounded == true)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        //Hacer el movimiento
        float horizontalVeloctiy = _movement.normalized.x * speed;
        _rigidbody.velocity = new Vector2(horizontalVeloctiy, _rigidbody.velocity.y);
    }

    private void LateUpdate()
    {
        //Comprobar las animaciones
        _animator.SetBool("Idle", _movement == Vector2.zero);
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetFloat("VerticalVelocity", _rigidbody.velocity.y);

    }

    //Hace el giro del personaje
    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.y);
    }

    //Permite saltar
    private void Jump()
    {
        _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _isGrounded = false; 
    }

    //Revisa las collisiones del personaje
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer ==6) 
        {
          _isGrounded = true;
        }
    }
}
