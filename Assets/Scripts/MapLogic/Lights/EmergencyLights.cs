using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Light State Enum
public enum LightsState
{
    Off = 0,
    Emergency,
    Normal
};
#endregion

public class EmergencyLights : MonoBehaviour {

    #region Enum
    [SerializeField]
    private LightsState lightState;
    #endregion

    #region Getters
    public LightsState GetLightState()
    {
        return lightState;
    }
    #endregion

    #region Setters
    public void SetLightState(LightsState lightState)
    {
        this.lightState = lightState;
    }
    #endregion
}
