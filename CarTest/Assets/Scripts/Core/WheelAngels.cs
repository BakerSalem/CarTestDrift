using System.Collections;
using UnityEngine;

namespace Car.Core
{
    public class WheelAngels : MonoBehaviour
    {
        #region Variables
        public GameObject wheelBase;
        public GameObject wheelGraphic;
        public WheelCollider wheelCol;
        public bool isSteerable;
        public float steerableAngle;
        RaycastHit hit;
        #endregion

        #region Setup Unity 
        void Update()
        {
            AlignMeshToCollider();
        }
        #endregion

        #region Methods
        void AlignMeshToCollider()
        {
            if (Physics.Raycast(wheelCol.transform.position, -wheelCol.transform.up, 
                out hit, wheelCol.suspensionDistance + wheelCol.radius))
            {
                wheelGraphic.transform.position = hit.point + wheelCol.transform.up * wheelCol.radius;
            }
            else
            {
                wheelGraphic.transform.position = wheelCol.transform.position - 
                    (wheelCol.transform.up * wheelCol.suspensionDistance);
            }

            if (isSteerable)
            {
                wheelCol.steerAngle = steerableAngle;
            }
            wheelGraphic.transform.eulerAngles =
                new Vector3(wheelBase.transform.eulerAngles.x, wheelBase.transform.eulerAngles.y
                + wheelCol.steerAngle, wheelBase.transform.eulerAngles.z);
            wheelGraphic.transform.Rotate
                (wheelCol.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        }
        #endregion
    }
}