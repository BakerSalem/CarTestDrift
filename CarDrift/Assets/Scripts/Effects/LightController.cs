using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Car.Effects
{
    public class LightController : MonoBehaviour
    {
        #region Variables
        public Material LightsMatrial;
        public float brake;
        #endregion

        #region Setup Unity
        void Update()
        {
            brake = Input.GetAxis("Vertical");
            LightManager();
        }

        #endregion

        #region Methodes
        public void LightManager()
        {

            if (brake <= -0.1f || Input.GetKey(KeyCode.Space))
            {
                LightsMatrial.EnableKeyword("_EMISSION");
            }
            else
            {
                LightsMatrial.DisableKeyword("_EMISSION");
            }

        }
        #endregion   
    }
}
