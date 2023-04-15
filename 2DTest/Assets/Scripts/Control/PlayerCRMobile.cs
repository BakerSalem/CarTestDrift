using System.Collections.Generic;
using UnityEngine;


namespace TD.Control
{
    public class PlayerCRMobile : MonoBehaviour
    {
        public Rigidbody2D rb;
        private Animator anim;
        //For Movement  -----------------------------------------------------------
        public Transform groundCheck;
        public Transform wallCheck;
        public LayerMask whatIsGround;

        private int facingDirection = 1;
        private float movementInputDirection;
        public float movementSpeed = 10.0f;
        public float movementForceInAir;
        public float airDragMultiplier = 0.95f;
        public float groundCheckRadius;

        private bool isFacingRight = true;
        private bool isWalking;
        private bool isGrounded;
        private bool canMove;
        private bool canFlip;
        private bool moveRight;
        private bool moveLeft;

        //For Jump  -----------------------------------------------------------
        public int amountOfJumps = 1;
        private int amountOfJumpsLeft;
        private int lastWallJumpDirection;
        public float jumpForce = 16.0f;
        public float variableJumpHeightMultiplier = 0.5f;
        private float jumpTimer;
        private float turnTimer;
        public float jumpTimerSet = 0.15f;
        public float turnTimerSet = 0.1f;
        public float wallJumpTimerSet = 0.5f;

        private bool hasWallJumped;
        private bool canNormalJump;
        private bool isTringToJump;
        private bool checkJumpMultiplier;

        //For WALLJUMP  -----------------------------------------------------------
        public Vector2 wallHopDirection;
        public Vector2 wallJumpDirection;

        public float wallHopForce;
        public float wallJumpForce;
        public float wallCheckDistance;
        private float wallJumpTimer;
        public float wallSlideSpeed;

        private bool canWallJump;
        private bool isTouchingWall;
        private bool isWallSliding;

        //For Rolling -----------------------------------------------------------
        private bool isRolling;
        private float RollTimeCounter;
        private float lastRoll = -100f;
        public float RollTime;
        public float RollSpeed;
        public float RollCoolDown;




        // Start is called before the first frame update
        public void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            amountOfJumpsLeft = amountOfJumps;
            wallHopDirection.Normalize();
            wallJumpDirection.Normalize();

        }

        // Update is called once per frame
        public void Update()
        {
            CheckInput();
            CheckMovementDirection();
            UpdateAnimations();
            CheckIfCanJump();
            //CheckIfWallSliding();
            CheckJump();
            CheckRolling();
            TryMove();

        }

        public void FixedUpdate()
        {
            ApplyMovement();
            CheckSurroundings();
        }

        // public void CheckIfWallSliding()
        // {
        //     if (isTouchingWall && movementInputDirection == facingDirection && rb.velocity.y < 0)
        //     {
        //         isWallSliding = true;
        //     }
        //     else
        //     {
        //         isWallSliding = false;
        //     }

        // }

        public void CheckSurroundings()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

            isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        }

        public void CheckIfCanJump()
        {
            if (isGrounded && rb.velocity.y <= 0.01f)
            {
                amountOfJumpsLeft = amountOfJumps;
            }

            if (isTouchingWall)
            {
                checkJumpMultiplier = false;
                canWallJump = true;
            }

            if (amountOfJumpsLeft <= 0)
            {
                canNormalJump = false;
            }
            else
            {
                canNormalJump = true;
            }

        }

        public void CheckMovementDirection()
        {
            if (isFacingRight && movementInputDirection < 0)
            {
                Flip();
            }
            else if (!isFacingRight && movementInputDirection > 0)
            {
                Flip();
            }

            if ((rb.velocity.x >= 0.1f || rb.velocity.x <= -0.1f) && !isRolling)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }
        }

        public void UpdateAnimations()
        {
            anim.SetBool("isWalking", isWalking);
            anim.SetBool("isGrounded", isGrounded);
            anim.SetFloat("yVelocity", rb.velocity.y);
            anim.SetBool("isWallSliding", isWallSliding);
            anim.SetBool("isRolling", isRolling);
        }

        public void CheckInput()
        {
            movementInputDirection = Input.GetAxisRaw("Horizontal");
            if (Input.GetButtonDown("Horizontal"))
            {
                if (isTouchingWall)
                {
                    if (!isGrounded && movementInputDirection != facingDirection)
                    {
                        canMove = false;
                        canFlip = false;
                        moveLeft = false;
                        moveRight = false;

                        turnTimer = turnTimerSet;
                    }
                }
            }

            if (!canMove)
            {
                turnTimer -= Time.deltaTime;

                if (turnTimer <= 0)
                {
                    canMove = true;
                    canFlip = true;
                }
            }


            if (Input.GetButtonDown("Jump"))
            {
                TryJump();
            }

            else if (!Input.GetButton("Jump"))
            {
                HoldJmup();
            }


            if (Input.GetButtonDown("Rolling"))
            {
                if (Time.time >= (lastRoll + RollCoolDown))
                    TryToRoll();

            }

        }
        public void DownLeft()
        {
            moveLeft = true;

        }
        public void UpLeft()
        {
            moveLeft = false;
        }
        public void DownRight()
        {
            moveRight = true;
        }
        public void UpRight()
        {
            moveRight = false;
        }

        public void TryMove()
        {
            if (moveLeft)
            {
                movementInputDirection = -movementSpeed;

            }
            else if (moveRight)
            {
                movementInputDirection = movementSpeed;
            }
            else
            {
                movementInputDirection = 0;
            }
            CheckMovementDirection();

            if (isTouchingWall && (moveRight || moveLeft) == rb.velocity.y < 0)
            {
                isWallSliding = true;
            }
            else
            {
                isWallSliding = false;
            }

        }

        public void TryJump()
        {
            if (isGrounded || (amountOfJumpsLeft > 0 && isTouchingWall))
            {
                NormalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isTringToJump = true;
            }

        }
        public void HoldJmup()
        {
            checkJumpMultiplier = false;
            if (checkJumpMultiplier)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
            }
        }

        public void TryToRoll()
        {
            isRolling = true;
            RollTimeCounter = RollTime;
            lastRoll = Time.time;
        }


        public void CheckRolling()
        {
            if (isRolling)
            {
                if (RollTimeCounter > 0 && isGrounded)
                {
                    canMove = false;
                    canFlip = false;
                    isWalking = false;
                    rb.velocity = new Vector2(RollSpeed * facingDirection, 0.0f);
                    RollTimeCounter -= Time.deltaTime;
                }

                if (RollTimeCounter <= 0 || isTouchingWall || !isGrounded)
                {
                    isRolling = false;
                    canMove = true;
                    canFlip = true;
                }
            }
        }

        public void CheckJump()
        {
            if (jumpTimer > 0)
            {
                //WallJump
                if (!isGrounded && isTouchingWall && movementInputDirection != 0 && movementInputDirection != facingDirection)
                {
                    WallJump();
                }
                else if (isGrounded)
                {
                    NormalJump();
                }
            }

            if (isTringToJump)
            {
                jumpTimer -= Time.deltaTime;
            }

            if (wallJumpTimer > 0)
            {
                if (hasWallJumped && movementInputDirection == -lastWallJumpDirection)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                    hasWallJumped = false;
                }
                else if (wallJumpTimer <= 0)
                {
                    hasWallJumped = false;
                }
                else
                {
                    wallJumpTimer -= Time.deltaTime;
                }
            }
        }

        public void NormalJump()
        {
            if (canNormalJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                amountOfJumpsLeft--;
                jumpTimer = 0;
                isTringToJump = false;
                checkJumpMultiplier = true;
            }
        }

        public void WallJump()
        {
            if (canWallJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                isWallSliding = false;
                amountOfJumpsLeft = amountOfJumps;
                amountOfJumpsLeft--;
                Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
                rb.AddForce(forceToAdd, ForceMode2D.Impulse);
                jumpTimer = 0;
                isTringToJump = false;
                checkJumpMultiplier = true;
                turnTimer = 0;
                canMove = true;
                canFlip = true;
                hasWallJumped = true;
                wallJumpTimer = wallJumpTimerSet;
                lastWallJumpDirection = -facingDirection;

            }
        }

        public void ApplyMovement()
        {


            if (!isGrounded && !isWallSliding && movementInputDirection == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
            }
            else if (canMove)
            {
                //rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
                rb.velocity = new Vector2(movementInputDirection, rb.velocity.y);

            }

            if (isWallSliding)
            {
                if (rb.velocity.y < -wallSlideSpeed)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
                }
            }
        }

        public void Flip()
        {
            if (!isWallSliding && canFlip)
            {
                facingDirection *= -1;
                isFacingRight = !isFacingRight;
                transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }

        public void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        }
    }
}
