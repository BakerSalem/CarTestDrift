using System.Collections;
using UnityEngine;

namespace Car.Core
{
    public class WheelManager : MonoBehaviour
    {

        #region Variables

        public float horizontal;
        public float vertical;
        public float breakPower;

        #region Steering Variables

        public float wheelRotateSpeed;
        public float wheelSteertableAngle;

        #endregion

        #region Motor Variables

        public float wheelAcceleration;
        public float wheelMaxSpeed;

        #endregion

        #region Unity Variables

        public WheelAngels[] steerableWheels;
        public Rigidbody rb;

        #endregion

        #endregion

        #region Setup Unity
        void Update()
        {
            WheelControl();
        }
        #endregion

        #region Methods
        void WheelControl()
        {
            for (int i = 0; i < steerableWheels.Length; i++)
            {
                //Default steering angle
                steerableWheels[i].steerableAngle =
                    Mathf.LerpAngle(steerableWheels[i].steerableAngle, 0,
                    Time.deltaTime * wheelRotateSpeed);
                //Default motor speed
                steerableWheels[i].wheelCol.motorTorque =
                    -Mathf.Lerp(steerableWheels[i].wheelCol.motorTorque, 0,
                    Time.deltaTime * wheelAcceleration);

                horizontal = Input.GetAxis("Horizontal");
                vertical = Input.GetAxis("Vertical");

                if (vertical > 0.1)
                {
                    steerableWheels[i].wheelCol.motorTorque =
                        -Mathf.Lerp(steerableWheels[i].wheelCol.motorTorque,
                        wheelMaxSpeed, Time.deltaTime * wheelAcceleration);

                }
                if (vertical < -0.1)
                {
                    steerableWheels[i].wheelCol.motorTorque =
                        Mathf.Lerp(steerableWheels[i].wheelCol.motorTorque,
                        wheelMaxSpeed, Time.deltaTime * wheelAcceleration * breakPower);

                    rb.drag = 0.3f;
                }
                else
                {
                    rb.drag = 0;
                }
                if (horizontal > 0.1)
                {
                    steerableWheels[i].steerableAngle =
                        Mathf.LerpAngle(steerableWheels[i].steerableAngle, -wheelSteertableAngle,
                        Time.deltaTime * wheelRotateSpeed);
                }

                if (horizontal < -0.1)
                {
                    steerableWheels[i].steerableAngle =
                        Mathf.LerpAngle(steerableWheels[i].steerableAngle, wheelSteertableAngle,
                        Time.deltaTime * wheelRotateSpeed);
                }
            }
            if (Input.GetKey(KeyCode.Space))
            {
                for (int i = 0; i < steerableWheels.Length; i++)
                {
                    steerableWheels[i].wheelCol.brakeTorque = wheelMaxSpeed * breakPower;
                }
                rb.drag = 2.0f;
            }
            else
            {
                for (int i = 0; i < steerableWheels.Length; i++)
                {
                    steerableWheels[i].wheelCol.brakeTorque = 0.0f;
                }
                rb.drag = 0.0f;
            }

        }
        #endregion
    }
}