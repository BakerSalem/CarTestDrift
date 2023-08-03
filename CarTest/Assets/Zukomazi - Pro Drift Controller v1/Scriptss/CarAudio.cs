using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour
{
    public AudioSource Engine;
    public SpeedCalculator speedCalculator;

    public AudioSource GearChangeSound;


    public float PitchOffSet1;
    public float PitchOffSet2;
    public float PitchOffSet3;
    public float PitchOffSet4;
    public float PitchOffSet5;
    public float PitchOffSet6;

    void Start()
    {

    }

    void Update()
    {
        PitchControl();
        GearChange();
        EngineVolume();
    }


    public void EngineVolume()
    {

            if (Input.GetAxis("Vertical") == 1)
            {
                Engine.volume += Time.deltaTime;
            }
            else
            {
                if (Engine.volume > 0.1f)
                {
                    Engine.volume -= Time.deltaTime;
                }
            }      

    }


    public void GearChange()
    {
        if (speedCalculator.Speed > 30 & speedCalculator.Speed < 31)
        {
            if(GearChangeSound.isPlaying == false)
            {
                GearChangeSound.Play();
            }
        }

        if (speedCalculator.Speed > 60 & speedCalculator.Speed < 61)
        {
            if (GearChangeSound.isPlaying == false)
            {
                GearChangeSound.Play();
            }
        }

        if (speedCalculator.Speed > 90 & speedCalculator.Speed < 91)
        {
            if (GearChangeSound.isPlaying == false)
            {
                GearChangeSound.Play();
            }
        }

        if (speedCalculator.Speed > 120 & speedCalculator.Speed < 121)
        {
            if (GearChangeSound.isPlaying == false)
            {
                GearChangeSound.Play();
            }
        }

        if (speedCalculator.Speed > 150 & speedCalculator.Speed < 151)
        {
            if (GearChangeSound.isPlaying == false)
            {
                GearChangeSound.Play();
            }
        }

        if (speedCalculator.Speed > 180 & speedCalculator.Speed < 181)
        {
            if (GearChangeSound.isPlaying == false)
            {
                GearChangeSound.Play();
            }
        }
    }

    public void PitchControl()
    {
        if (speedCalculator.Speed > 0 & speedCalculator.Speed < 30)
        {
            Engine.pitch = speedCalculator.Speed * PitchOffSet1;
        }

        if (speedCalculator.Speed > 30 & speedCalculator.Speed < 60)
        {
            Engine.pitch = speedCalculator.Speed * PitchOffSet2;
        }

        if (speedCalculator.Speed > 60 & speedCalculator.Speed < 90)
        {
            Engine.pitch = speedCalculator.Speed * PitchOffSet3;
        }

        if (speedCalculator.Speed > 90 & speedCalculator.Speed < 120)
        {
            Engine.pitch = speedCalculator.Speed * PitchOffSet4;
        }

        if (speedCalculator.Speed > 120 & speedCalculator.Speed < 150)
        {
            Engine.pitch = speedCalculator.Speed * PitchOffSet5;
        }

        if (speedCalculator.Speed > 150)
        {
            Engine.pitch = speedCalculator.Speed * PitchOffSet6;
        }
    }


}
