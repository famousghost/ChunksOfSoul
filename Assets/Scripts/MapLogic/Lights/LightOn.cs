using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOn : MonoBehaviour {

    #region Light
    [SerializeField]
    private Light corridorLight;
    #endregion

    #region EmergencyLights
    [SerializeField]
    private EmergencyLights emergencyLights;
    #endregion

    #region System Methods
    // Use this for initialization
    void Start () {
        corridorLight = GetComponent<Light>();
        emergencyLights = GameObject.FindGameObjectWithTag("LightState").GetComponent<EmergencyLights>();
	}
	
	// Update is called once per frame
	void Update () {
        if (emergencyLights.GetLightState() == LightsState.Emergency)
        {
            corridorLight.color = Color.red;
            corridorLight.enabled = true;
        }
        if(emergencyLights.GetLightState() == LightsState.Normal)
        {
            corridorLight.color = Color.white;
            corridorLight.enabled = true;
        }
	}
    #endregion
}
