using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowD.Control
{
    public class RollingManager : MonoBehaviour
    {
        #region Variables
        Rigidbody2D rb;
        PlayerMovement playerMovement;
        WallJump wallJump;

        public bool isRolling;
        public float RollTimeCounter;
        public float lastRoll = -100f;
        public float RollTime = 1f;
        public float RollSpeed = 15f;
        public float RollCoolDown = 1f;
        #endregion

        #region Unity Setup
        private void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();
            wallJump = GetComponent<WallJump>();
            rb = GetComponent<Rigidbody2D>();
        }
        #endregion

        #region Public Functions
        public void RollingInput()
        {
            if (Input.GetButtonDown("Rolling"))
            {
                if (Time.time >= (lastRoll + RollCoolDown))
                    TryToRoll();
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
                if (RollTimeCounter > 0 && playerMovement.isGrounded)
                {
                    playerMovement.canMove = false;
                    playerMovement.canFlip = false;
                    playerMovement.isWalking = false;
                    rb.velocity = new Vector2(RollSpeed * playerMovement.facingDirection, 0.0f);
                    RollTimeCounter -= Time.deltaTime;
                }

                if (RollTimeCounter <= 0 || wallJump.isTouchingWall || !playerMovement.isGrounded)
                {
                    isRolling = false;
                    playerMovement.canMove = true;
                    playerMovement.canFlip = true;
                }
            }
        }
        #endregion
    }

}
