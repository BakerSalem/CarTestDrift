using System.Collections.Generic;
using UnityEngine;

namespace TD.Control
{
    public class InputControl : MonoBehaviour
    {
        public bool moveRight;
        public bool moveLeft;
        public float movementSpeed = 10.0f;
        public float HDirection;
        public Rigidbody2D rbg;

        private void Start()
        {
            moveRight = false;
            moveLeft = false;

        }
        private void FixedUpdate()
        {
            rbg.velocity = new Vector2(HDirection, rbg.velocity.y);
        }

       

        public void TryWalk()
        {
           
        }

    }
}