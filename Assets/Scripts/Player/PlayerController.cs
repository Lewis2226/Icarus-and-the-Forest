using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //Varaibles movimiento y salto
    public float speed = 3f;
    public float jumpForce = 8f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float horizontalInput;


    //Variables doble salto
    private bool dobleJump;

    //Variables salto cargado 
    private float jumpTimerCounter;
    public float jumptime;
    private bool isJumping;


    //Variables  deslizamiento en pared 
    public float slideSpeed = 1f;
    public Transform wallCheck;
    public LayerMask wallLayer;

    //Variables salto en pared 
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCouter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 8f);


    //Variables dash
    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 100f;
    private float dashTime = 1f;
    private float dashingCooldown = 1f;

    //Referencias
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    public TrailRenderer trailRenderer;
    //Movimiento 
    private Vector2 _movement;
    private bool _facingRight = true;
    private bool _isGrounded;
    private bool _onWall;
    private bool isWallSlide;

    //Otras
    public float radius;

    private void Awake() //Se toman los componentes necesarios
                         
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }



    void Update()
    {
       if (isDashing)
        {
            return;
        }
        //Registar el Movimiento 
        horizontalInput = Input.GetAxisRaw("Horizontal");
        _movement = new Vector2(horizontalInput, 0);

        
        if (_isGrounded)
        {
            dobleJump = false;
        }
        else
        {
            dobleJump = true;
        }
        //Salto
        if (Input.GetButtonDown("Jump") && _isGrounded == true || Input.GetButtonDown("Jump") && dobleJump)
        {
            Jump();
            isJumping = true;
            jumpTimerCounter = jumptime;
        }
        //Salto Cargado 
        if (Input.GetButton("Jump") && isJumping == true)
        {
            if(jumpTimerCounter > 0)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
                jumpTimerCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        //Deslizamiento en pared 
         WallSlide();
        //Salto en pared
         WallJump();

        if (!isWallJumping)
        {
            //Hacer que el personaje gire
            if (horizontalInput < 0f && _facingRight == true)
            {
                Flip();
            }
            else if (horizontalInput > 0f && _facingRight == false)
            {
                Flip();
            }
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) 
        {
           StartCoroutine(Dash());
        }

        //caida con planeo

        //Revisa si esta en el piso 
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);

        //Revisa si esta en pared
        _onWall = Physics2D.OverlapCircle(wallCheck.position, radius, wallLayer);
    }

    private void FixedUpdate()
    {
        //Hacer el movimiento
        float horizontalVeloctiy = _movement.normalized.x * speed;
        _rigidbody.velocity = new Vector2(horizontalVeloctiy, _rigidbody.velocity.y);

        if(isDashing)
        {
            return;
        }
    }

    private void LateUpdate()
    {
        //Comprobar las animaciones
        _animator.SetBool("Idle", _movement == Vector2.zero);
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetBool("OnWall", _onWall);
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
        _rigidbody.velocity = new Vector2 (_rigidbody.velocity.x,jumpForce);
        _isGrounded = false;
        dobleJump = false;
    }

    // deslizamiento en pared 
    private void WallSlide()
    {
        if (_onWall && !_isGrounded)
        {
            isWallSlide = true;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Clamp(_rigidbody.velocity.y, -slideSpeed, float.MaxValue)); 
        }
        else
        {
            isWallSlide = false;
        }
    }

    //Salto en pared
    private void WallJump()
    {
        if (isWallSlide )
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCouter = wallJumpingTime;
            CancelInvoke("StopWallJumping");
        }
        else
        {
            wallJumpingCouter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCouter > 0)
        {
            isWallJumping = true;
            _rigidbody.velocity = new Vector2( wallJumpingDirection* wallJumpingPower.x , wallJumpingPower.y);
            wallJumpingCouter = 0;

            if (transform.localScale.x != wallJumpingDirection)
            {
                _facingRight = !_facingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }
            Invoke("StopWallJumping", wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    //Dash
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float orginalGravity = _rigidbody.gravityScale;
        _rigidbody.gravityScale = 0;
        _rigidbody.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashTime);
        trailRenderer.emitting = false;
        _rigidbody.gravityScale = orginalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true; 
    }
 
}
