using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpeedCalculator : MonoBehaviour
{
    public float Speed;
    public Rigidbody rb;

    public Text speedText;

    void FixedUpdate()
    {
        Vector3 vel = rb.velocity;
        Speed = vel.magnitude * 2.23693629f;
        speedText.text = Speed.ToString("0");
    }


}
