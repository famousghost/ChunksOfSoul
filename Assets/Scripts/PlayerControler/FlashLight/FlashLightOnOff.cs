using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightOnOff : MonoBehaviour
{
    #region Light
    [SerializeField]
    private Light flashLight;
    #endregion

    [SerializeField]
    private FlashLightState flashLightState;

    #region System Methods
    // Use this for initialization
    void Start () {
        flashLight = this.GetComponent<Light>();
        flashLightState = GameObject.FindGameObjectWithTag("FlashLight").GetComponent<FlashLightState>();
    }
	
	// Update is called once per frame
	void Update () {
        OnOffMethod();
    }
    #endregion

    #region OnOffMethod
    private void OnOffMethod()
    {
        if (flashLightState.flashLightOn)
        {
            flashLight.enabled = true;
        }
        else
        {
            flashLight.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashLightState.flashLightOn)
            {
                flashLightState.flashLightOn = false;
            }
            else
            {
                flashLightState.flashLightOn = true;
            }
            flashLightState.updateFlashLightStateOnServer();
        }
    }

    public void flashLigthOn()
    {
        flashLight.enabled = true;
    }

    public void flashLigthOff()
    {
        flashLight.enabled = false;
    }
    #endregion
}
