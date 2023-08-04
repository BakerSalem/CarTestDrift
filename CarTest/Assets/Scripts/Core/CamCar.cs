using UnityEngine;

namespace Car.Core
{
    public class CamCar : MonoBehaviour
    {
        #region Variables

        #region Private 
        private Transform stackNode;
        private Transform carCam;
        private Transform car;
        private Rigidbody carPhysics;
        #endregion

        #region Public
        public float rotationThreshold = 1f;
        public float camerSticking = 10.0f;
        public float cameraRotationSpeed = 5.0f;
        #endregion

        #endregion

        #region Setup Unity
        void Awake()
        {
            carCam = Camera.main.GetComponent<Transform>();
            stackNode = GetComponent<Transform>();
            car = stackNode.parent.GetComponent<Transform>();
            carPhysics = car.GetComponent<Rigidbody>();
        }

        void Start()
        {
            stackNode.parent = null;
        }

        void FixedUpdate()
        {
            Quaternion look;

            stackNode.position = Vector3.Lerp(stackNode.position, car.position, camerSticking * Time.fixedDeltaTime);

            if (carPhysics.velocity.magnitude < rotationThreshold)
            {
                look = Quaternion.LookRotation(car.forward);
            }
            else
            {
                look = Quaternion.LookRotation(carPhysics.velocity.normalized);
            }
            look = Quaternion.Slerp(stackNode.rotation, look, cameraRotationSpeed * Time.fixedDeltaTime);
            stackNode.rotation = look;
        }
        #endregion
    }

}
