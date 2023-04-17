using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    #region Variables

    #region Unity Component
    Rigidbody2D rb;
    NormalJump normalJump;
    PlayerMovement playerMove;
    #endregion

    #region Jump Direction
    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;
    #endregion

    #region Jump Force
    public float wallHopForce = 10f;
    public float wallJumpForce = 20f;
    public float wallCheckDistance = 0.3f;
    public float wallJumpTimer = 0.5f;
    public float wallSlideSpeed = 2f;
    #endregion

    #region Wall Jump State
    public bool canWallJump;
    public bool isTouchingWall;
    public bool isWallSliding;
    #endregion

    #endregion

    #region Unity Setup
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMovement>();
        normalJump = GetComponent<NormalJump>();
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }
    #endregion


    #region Public Functions
    public void wallJump(float moveDir)
    {
        if (canWallJump && isWallSliding )
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            isWallSliding = false;
            normalJump.amountOfJumpsLeft = normalJump.amountOfJumps;
            normalJump.amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * moveDir, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            normalJump.jumpTimer = 0;
            normalJump.isTringToJump = false;
            normalJump.checkJumpMultiplier = true;
            normalJump.turnTimer = 0;
            playerMove.canMove = true;
            playerMove.canFlip = true;
            normalJump.hasWallJumped = true;
            wallJumpTimer = normalJump.wallJumpTimerSet;
            normalJump.lastWallJumpDirection = -playerMove.facingDirection;

        }
    }

    public void CheckIfWallSliding(float moveDir,float faceDir,bool isGrounded)
    {
        // && moveDir == faceDir
        if (isTouchingWall && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

    }
    #endregion
}
