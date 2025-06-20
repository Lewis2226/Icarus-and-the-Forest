using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    //Varaibles movimiento y salto
    public float speed = 3f;
    public float jumpForce = 8f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float horizontalInput;


    //Variables doble salto
    public bool dobleJump;



    //Variables salto cargado 
    private float jumpTimerCounter;
    public float jumptime;
    private bool isJumping;
    public float jumpForceChager;


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
    private Vector2 wallJumpingPower = new Vector2(6f, 8f);


    //Variables dash
    public float direccion;
    public bool canDash = true;
    private bool isDashing;
    private float dashPower = 4f;
    public float dashTime = 1f;
    private float dashingCooldown = .5f;

    //Variables Coyote time
    public float coyoteTime = .3f;
    private float coyoteTimeCounter;

    //Referencias
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    public TrailRenderer trailRenderer;

    //Movimiento 
    private Vector2 _movement;
    public bool _facingRight = true;
    private bool _isGrounded;
    private bool _onWall;
    private bool isWallSlide;

    //Planear
    float orginalGravity;
    public bool isPlannig;

    //Otras
    public float radius;

    //Particulas 
    public GameObject Particulas;

    private void Awake() //Se toman los componentes necesarios

    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
    }

    public void Start()
    {
         orginalGravity = _rigidbody.gravityScale;
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
            isPlannig = false;
            _rigidbody.gravityScale = orginalGravity;
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }


        //Salto
        if (Input.GetButtonDown("Jump"))
        {
            if (_isGrounded)
            {
                Jump();
                isJumping = true;
                jumpTimerCounter = jumptime;
            }

            //Coyote time
            if (coyoteTimeCounter > 0)
            {
                Jump();
            }

            //Doble salto
            else if (!dobleJump)
            {
                _animator.SetBool("DobleJump", true);
                dobleJump = true;
                Jump();
            }
        }

        //Planear
        if (Input.GetKey(KeyCode.LeftControl) && !isPlannig && !_isGrounded)
        {
             _rigidbody.gravityScale = 0.10f;
            Plan();
        }
        else
        {
            isPlannig = false;
            _rigidbody.gravityScale = orginalGravity;
            isPlannig = false;
        }
       
        
            //Salto Cargado 
            if (Input.GetButton("Jump") && isJumping == true)
            {
                if (jumpTimerCounter > 0)
                {
                    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForceChager);
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

          


            //Revisa si esta en el piso 
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);

            //Revisa si esta en pared
            _onWall = Physics2D.OverlapCircle(wallCheck.position, radius, wallLayer);
        }

        private void FixedUpdate()
        {
          //Hacer el movimiento
           float horizontalVeloctiy;
           if(_movement.x < 0) 
           {
            direccion = -1;
           }
           else if (_movement.x > 0) { }
           {
            direccion = 1;
           }


        if (_onWall)
        {
            horizontalVeloctiy = _movement.x * 0;
        }
        else
        {
            horizontalVeloctiy = _movement.x * speed;
            
        }


        if (isDashing)
        {
            //Debug.Log("Estoy haciendo el dash");
        }
        else
        {
            _rigidbody.velocity = new Vector2(horizontalVeloctiy, _rigidbody.velocity.y);
        }
            if (isDashing)
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
        _animator.SetBool("Dash", isDashing);
        _animator.SetBool("DobleJump", false);
        _animator.SetBool("Plane", isPlannig);
    }

    //Hace el giro del personaje
    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

    }

        //Permite saltar
        private void Jump()
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
            _isGrounded = false;
            Particulas.SetActive(true);
            Invoke("ParticleOff", 1.5f);

        }

        // deslizamiento en pared 
        private void WallSlide()
        {
            if (_onWall && !_isGrounded)
            {
                isWallSlide = true;
              _rigidbody.gravityScale = .30f;
            } else

            {
              isWallSlide= false;
              _rigidbody.gravityScale = orginalGravity;
            }
            
        }

        //Salto en pared
        private void WallJump()
        {
            if (isWallSlide)
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
                _rigidbody.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
                wallJumpingCouter = 0;
                dobleJump = false;


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
            _rigidbody.gravityScale = 0;
        if (_facingRight)
        {
            _rigidbody.velocity = new Vector2(direccion * dashPower, 0f);
        }
        else
        {
            _rigidbody.velocity = new Vector2(direccion * -dashPower, 0f);
        }
        trailRenderer.emitting = true;
            yield return new WaitForSeconds(dashTime);
            isDashing = false;
            trailRenderer.emitting = false;
            _rigidbody.gravityScale = orginalGravity;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }

        //Planear
          private void Plan()
          {
            isPlannig = true;
          }

         private void ParticleOff()
         {
           Particulas.SetActive(false);
         }
} 


