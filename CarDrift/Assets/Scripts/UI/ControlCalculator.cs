using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Car.UI
{
    public class ControlCalculator : MonoBehaviour
    {
        #region Variables
        public Text speedText;
        public Rigidbody rb;
        public float speed;
        #endregion

        #region Setup Unity
        void FixedUpdate()
        {
            Vector3 vec = rb.velocity;
            speed = vec.magnitude * 2.23693629f;
            speedText.text = speed.ToString("0");
        }
        #endregion
    }

}
