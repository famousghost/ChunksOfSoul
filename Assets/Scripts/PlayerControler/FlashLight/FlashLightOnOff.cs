using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightOnOff : FlashLightSound {

    #region Light
    [SerializeField]
    private Light flashLight;
    #endregion

    #region Float
    [SerializeField]
    private float batteryPowerLevel = 100.0f;

    [SerializeField]
    private float timeToBatteryLow = BATTERYLOWTIME;
    #endregion

    #region Const
    private const float BATTERYLOWTIME = 300.0f;
    #endregion

    #region System Methods
    // Use this for initialization
    void Start () {

        flashLightSource = GetComponent<AudioSource>();
        flashLightOn = Resources.Load("FlashLight/flashlighton", typeof(AudioClip)) as AudioClip;
        flashLightOff = Resources.Load("FlashLight/flashlightoff", typeof(AudioClip)) as AudioClip;
        flashLight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        OnOffMethod();
    }
    #endregion

    #region OnOffMethod
    private void OnOffMethod()
    {
        CheckBatterLow();
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (batteryPowerLevel > 0.0f)
            {
                if (flashLight.enabled)
                {
                    PlayFlashLightOff();
                    flashLight.enabled = false;
                }
                else
                {
                    PlayFlashLightOn();
                    flashLight.enabled = true;
                }
            }
            else
            {
                PlayFlashLightOff();
                flashLight.enabled = false;
            }
        }
    }
    #endregion

    #region CheckBatteryLow
    private void CheckBatterLow()
    {
        if (flashLight.enabled)
        {
            if (timeToBatteryLow <= 0.0f)
            {
                batteryPowerLevel -= 20.0f;
                timeToBatteryLow = 300.0f;
            }
            timeToBatteryLow -= Time.deltaTime * 1.0f;
        }
        if(batteryPowerLevel <= 20.0f && timeToBatteryLow <= 10.0f)
        {
            flashLight.range = Random.Range(8.5f, 10.0f);
        }
        if(batteryPowerLevel <= 0.0f)
        {
            PlayFlashLightOff();
            flashLight.enabled = false;
        }
    }
    #endregion
}
