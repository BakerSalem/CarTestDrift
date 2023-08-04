using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Car.UI;

namespace Car.Crash
{
    public class TireDamageScript : MonoBehaviour
    {
        #region Variables

        public ControlCalculator player;
        public GameObject tireMesh;
        public GameObject tireRigid;
        public WheelCollider theWheelCollider;
        public GameObject popedTireSpawnPos;
        public float speed;
        public float tirePopSpeed;

        #endregion

        #region Setting
        public void OnTriggerEnter(Collider other)
        {
            speed = player.speed;

            if (speed > tirePopSpeed)
            {
                if (other.tag != "Player" && other.tag != "Trigger")
                {
                    tireMesh.SetActive(false);
                    Instantiate(tireRigid, popedTireSpawnPos.transform.position, popedTireSpawnPos.transform.rotation);
                    theWheelCollider.radius = 0;
                    gameObject.SetActive(false);
                }
            }

        }
        #endregion
    }

}
