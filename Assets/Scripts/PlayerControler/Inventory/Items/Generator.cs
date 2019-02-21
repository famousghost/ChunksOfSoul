using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    #region SwitchActive Class
    [SerializeField]
    private SwitchActive switchAcitve;
    #endregion

    #region Bool
    [SerializeField]
    private bool canUseGenerator = false;
    #endregion

    #region Emergency Lights Class
    [SerializeField]
    private EmergencyLights emergencyLights;
    #endregion

    #region System Methods
    // Use this for initialization
    void Start () {
        switchAcitve = GameObject.FindGameObjectWithTag("GeneratorPlace").GetComponent<SwitchActive>();
        emergencyLights = GameObject.FindGameObjectWithTag("LightState").GetComponent<EmergencyLights>();
    }
    #endregion

    #region Check If item is generator
    public bool CheckGeneratorUse(string nameOfItem)
    {
        if (nameOfItem == "generator-kits")
        {
            return true;
        }
    return false;
    }
    #endregion

    #region Use Generator
    public void UseGenerator()
    {
        switchAcitve.PlayPowerOnSound();
        emergencyLights.SetLightState(LightsState.Emergency);
    }
    #endregion

    #region Setters
    public void SetUseGenerator(bool canUseGenerator)
    {
        this.canUseGenerator = canUseGenerator;
    }
    #endregion

    #region Getters
    public bool GetUseGenerator()
    {
        return canUseGenerator;
    }
    #endregion
}
