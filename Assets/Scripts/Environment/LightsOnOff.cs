using UnityEngine;
using System.Collections;

public class LightsOnOff : MonoBehaviour {

    #region Class Variables

    [SerializeField]
    private Light lightBroke;

    #endregion

    #region Float Variables

    [SerializeField]
    private float timing = 3.0f;

    #endregion

    #region System Functions

    // Use this for initialization
    void Start () {
        //lightBroke = GameObject.FindGameObjectWithTag("broke").GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        Lightoff();
        Timer();
        LightOn();
    }

    #endregion

    #region Timer

    private void Timer()
    {
        timing -= (1.0f * Time.deltaTime);
    }

    #endregion

    #region LightScripts

    private void Lightoff()
    {
        if (timing >= 1.0f)
        {
            lightBroke.enabled = true;
        }
    }

    private void LightOn()
    {
        if (timing <= 0.0f)
        {
            lightBroke.enabled = false;
            timing = Random.Range(3.0f, 12.0f);
        }
    }
    #endregion

}
