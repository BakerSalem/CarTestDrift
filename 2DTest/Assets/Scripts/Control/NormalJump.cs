using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowD.Control
{
    public class NormalJump : MonoBehaviour
    {
        #region Variables

        #region Unity Components
        Rigidbody2D rb;
        WallJump wallJump;
        #endregion

        #region Jump Force 
        public int amountOfJumps = 1;
        public int amountOfJumpsLeft;
        public int lastWallJumpDirection;
        public float jumpForce = 16.0f;
        public float variableJumpHeightMultiplier = 0.5f;
        #endregion

        #region Jump Timer
        public float jumpTimer;
        public float turnTimer;
        public float jumpTimerSet = 0.15f;
        public float turnTimerSet = 0.1f;
        public float wallJumpTimerSet = 0.5f;
        #endregion

        #region Jump State
        public bool hasWallJumped;
        public bool canNormalJump;
        public bool isTringToJump;
        public bool checkJumpMultiplier;

        public bool doJump;
        #endregion

        #endregion

        #region Unity Setup
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            wallJump = GetComponent<WallJump>();
            amountOfJumpsLeft = amountOfJumps;
        }
        #endregion

        #region Public Functions
        public void CheckJump(bool IsGorund, float moveDir, float faceDir)
        {
            if (jumpTimer > 0)
            {
                //WallJump
                if (!IsGorund && wallJump.isTouchingWall && moveDir != 0 && moveDir != faceDir)
                {
                    wallJump.wallJump(moveDir);
                }
                else if (IsGorund)
                {
                    normalJump();
                }
            }

            if (isTringToJump)
            {
                jumpTimer -= Time.deltaTime;
            }

            if (wallJump.wallJumpTimer > 0)
            {
                if (hasWallJumped && moveDir == -lastWallJumpDirection)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                    hasWallJumped = false;
                }
                else if (wallJump.wallJumpTimer <= 0)
                {
                    hasWallJumped = false;
                }
                else
                {
                    wallJump.wallJumpTimer -= Time.deltaTime;
                }
            }
        }

        public void CheckIfCanJump(bool IsGorund)
        {
            if (IsGorund && rb.velocity.y <= 0.01f)
            {
                amountOfJumpsLeft = amountOfJumps;
            }

            if (wallJump.isTouchingWall && !IsGorund)
            {
                checkJumpMultiplier = false;
                wallJump.canWallJump = true;
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

        public void JumpInput(bool IsGorund)
        {
            if (Input.GetButtonDown("Jump"))
            {
                MakeJumpDown(IsGorund);
            }

            if (checkJumpMultiplier && Input.GetButtonUp("Jump"))
            {
                MakeJumpUp();
            }
        }

        public void normalJump()
        {
            if (canNormalJump)
            {
                doJump = true;
                checkJumpMultiplier = true;
                variableJumpHeightMultiplier += 0.1f;
            }
        }

        public void MakeJumpDown(bool isGround)
        {
            if (isGround || (amountOfJumpsLeft > 0 && wallJump.isTouchingWall))
            {
                normalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isTringToJump = true;
            }
        }

        public void MakeJumpUp()
        {
            if (doJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * variableJumpHeightMultiplier);
                variableJumpHeightMultiplier = 0.5f;
                amountOfJumpsLeft--;
                jumpTimer = 0;
                isTringToJump = false;
                checkJumpMultiplier = false;
                doJump = false;
            }
        }
        #endregion
    }

}
