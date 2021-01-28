using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightOnOff : FlashLightSound {

    #region Light
    [SerializeField]
    private Light flashLight;
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
        if (Input.GetKeyDown(KeyCode.F))
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
    }
    #endregion
}
