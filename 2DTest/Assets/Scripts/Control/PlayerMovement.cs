using System.Collections.Generic;
using UnityEngine;

namespace TowD.Control
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Variables

        #region Unity Components
        Rigidbody2D rb;
        Animator anim;
        NormalJump normalJump;
        WallJump wallJump;
        RollingManager rollingManager;
        #endregion

        #region Ground Check
        public Transform groundCheck;
        public Transform wallCheck;
        public LayerMask whatIsGround;
        public float groundCheckRadius;
        #endregion

        #region Movement Managment
        public int facingDirection = 1;
        public float movementInputDirection;
        public float movementSpeed = 10.0f;
        public float movementForceInAir;
        public float airDragMultiplier = 0.95f;
        #endregion

        #region Movement State
        public bool isFacingRight = true;
        public bool isWalking;
        public bool isGrounded;
        public bool canMove;
        public bool canFlip;

        public bool IsButtonClicked;
        #endregion

        #endregion

        #region Unity Setup
        // Start is called before the first frame update
        public void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            normalJump = GetComponent<NormalJump>();
            wallJump = GetComponent<WallJump>();
            rollingManager = GetComponent<RollingManager>();
        }

        public void FixedUpdate()
        {
            CheckInput();
            CheckMovementDirection();
            ApplyMovement();
            CheckSurroundings();
        }

        #endregion

        #region Private Functions
        void CheckSurroundings()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

            wallJump.isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallJump.wallCheckDistance, whatIsGround);
        }

        void CheckMovementDirection()
        {
            if (isFacingRight && movementInputDirection < 0)
            {
                Flip();
            }
            else if (!isFacingRight && movementInputDirection > 0)
            {
                Flip();
            }

            if ((rb.velocity.x >= 0.1f || rb.velocity.x <= -0.1f) && !rollingManager.isRolling)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }
        }

        void CheckInput()
        {
            if (!IsButtonClicked)
            {
                movementInputDirection = Input.GetAxisRaw("Horizontal");
            }


            normalJump.JumpInput(isGrounded);

            if (Input.GetButtonDown("Horizontal") && wallJump.isTouchingWall)
            {
                if (!isGrounded && movementInputDirection != facingDirection)
                {
                    canMove = false;
                    canFlip = false;

                    normalJump.turnTimer = normalJump.turnTimerSet;
                }
            }

            if (!canMove)
            {
                normalJump.turnTimer -= Time.deltaTime;

                if (normalJump.turnTimer <= 0)
                {
                    canMove = true;
                    canFlip = true;
                }
            }

            rollingManager.RollingInput();

        }

        void ApplyMovement()
        {

            if (!isGrounded && !wallJump.isWallSliding && movementInputDirection == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
            }
            else if (canMove)
            {
                rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
            }


            if (wallJump.isWallSliding)
            {
                if (rb.velocity.y < -wallJump.wallSlideSpeed)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -wallJump.wallSlideSpeed);
                }
            }
        }
        public void DisableFlip()
        {
            canFlip = false;
        }
        public void EnableFlip()
        {
            canFlip = true;
        }

        void Flip()
        {
            if (!wallJump.isWallSliding && canFlip)
            {
                facingDirection *= -1;
                isFacingRight = !isFacingRight;
                transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }

        #endregion

        #region Public Functions

        // Used OnButtons Click Component
        public void WalkRightButton()
        {
            IsButtonClicked = true;
            movementInputDirection = 1f;
        }
        // Used OnButtons Click Component
        public void WalkLeftButton()
        {
            IsButtonClicked = true;
            movementInputDirection = -1f;
        }
        // Used OnButtons Click Component
        public void ButtonUp()
        {
            IsButtonClicked = false;
            movementInputDirection = 0f;
        }

        public void OnDrawGizmos()
        {
            if (wallJump != null)
            {
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

                Gizmos.DrawLine(wallCheck.position,
                    new Vector3(wallCheck.position.x + wallJump.wallCheckDistance,
                    wallCheck.position.y, wallCheck.position.z));

            }

        }
        #endregion

    }
}
