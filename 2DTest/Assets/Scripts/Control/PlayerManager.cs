using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowD.Control
{
    public class PlayerManager : MonoBehaviour
    {
        #region Variables
        Rigidbody2D rb;
        Animator anim;
        PlayerMovement playerMovement;
        NormalJump normalJump;
        WallJump wallJump;
        RollingManager rollingManager;
        #endregion

        #region Unity Setup
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            playerMovement = GetComponent<PlayerMovement>();
            normalJump = GetComponent<NormalJump>();
            wallJump = GetComponent<WallJump>();
            rollingManager = GetComponent<RollingManager>();
        }

        private void FixedUpdate()
        {
            UpdateAnimations();
            normalJump.CheckIfCanJump(playerMovement.isGrounded);
            wallJump.CheckIfWallSliding(playerMovement.movementInputDirection, playerMovement.facingDirection, playerMovement.isGrounded);
            normalJump.CheckJump(playerMovement.isGrounded, playerMovement.movementInputDirection, playerMovement.facingDirection);
            rollingManager.CheckRolling();
        }
        #endregion

        #region Private Functions
        void UpdateAnimations()
        {
            anim.SetBool("isWalking", playerMovement.isWalking);
            anim.SetBool("isGrounded", playerMovement.isGrounded);
            anim.SetFloat("yVelocity", rb.velocity.y);
            anim.SetBool("isWallSliding", wallJump.isWallSliding);
            anim.SetBool("isRolling", rollingManager.isRolling);
        }
        #endregion
    }

}
