using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Car.UI;
using UnityEngine.UI;

namespace Car.Audio
{
    public class CarAudio : MonoBehaviour
    {
        #region Variables

        #region Unity Variables
        public AudioSource engine;
        public ControlCalculator controlCalculator;
        public AudioSource gearSwitchAudio;
        public Text gearSwitchText;
        #endregion

        #region Public Variables
        public float offSet1;
        public float offSet2;
        public float offSet3;
        public float offSet4;
        public float offSet5;
        public float offSet6;
        #endregion

        #endregion

        #region Setup Unity
        void Update()
        {
            OffSetControl();
            GearSwitch();
            EngineVolume();
        }
        #endregion

        #region Methods
        public void OffSetControl()
        {
            if (controlCalculator.speed > 0 & controlCalculator.speed < 30)
            {
                engine.pitch = controlCalculator.speed * offSet1;
            }
            if (controlCalculator.speed > 30 & controlCalculator.speed < 60)
            {
                engine.pitch = controlCalculator.speed * offSet2;
            }
            if (controlCalculator.speed > 60 & controlCalculator.speed < 90)
            {
                engine.pitch = controlCalculator.speed * offSet3;
            }
            if (controlCalculator.speed > 90 & controlCalculator.speed < 120)
            {
                engine.pitch = controlCalculator.speed * offSet4;
            }
            if (controlCalculator.speed > 120 & controlCalculator.speed < 150)
            {
                engine.pitch = controlCalculator.speed * offSet5;
            }
            if (controlCalculator.speed > 150)
            {
                engine.pitch = controlCalculator.speed * offSet6;
            }
        }
        public void GearSwitch()
        {
            if(controlCalculator.speed > 30 &controlCalculator.speed < 31)
            {
                if(gearSwitchAudio.isPlaying == false)
                {
                    gearSwitchAudio.Play();
                    gearSwitchText.text = "1";
                }
            }
            if (controlCalculator.speed > 60 & controlCalculator.speed < 61)
            {
                if (gearSwitchAudio.isPlaying == false)
                {
                    gearSwitchAudio.Play();
                    gearSwitchText.text = "2";
                }
            }
            if (controlCalculator.speed > 90 & controlCalculator.speed < 91)
            {
                if (gearSwitchAudio.isPlaying == false)
                {
                    gearSwitchAudio.Play();
                    gearSwitchText.text = "3";
                }
            }
            if (controlCalculator.speed > 120 & controlCalculator.speed < 121)
            {
                if (gearSwitchAudio.isPlaying == false)
                {
                    gearSwitchAudio.Play();
                    gearSwitchText.text = "4";
                }
            }
            if (controlCalculator.speed > 150 & controlCalculator.speed < 151)
            {
                if (gearSwitchAudio.isPlaying == false)
                {
                    gearSwitchAudio.Play();
                    gearSwitchText.text = "5";
                }
            }
            if (controlCalculator.speed > 180 & controlCalculator.speed < 181)
            {
                if (gearSwitchAudio.isPlaying == false)
                {
                    gearSwitchAudio.Play();
                }
            }
        }
        public void EngineVolume()
        {
            if(Input.GetAxis("Vertical") == 1)
            {
                engine.volume += Time.deltaTime;
            }
            else
            {
                if (engine.volume > 0.1f)
                {
                    engine.volume -= Time.deltaTime;
                }
            }
        }
        #endregion

    }

}
