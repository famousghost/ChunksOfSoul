using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightSound : MonoBehaviour {

    #region Audio Clips
    [SerializeField]
    protected AudioClip flashLightOn;

    [SerializeField]
    protected AudioClip flashLightOff;
    #endregion

    #region Audio Source
    [SerializeField]
    protected AudioSource flashLightSource;
    #endregion

    #region FlashLight On and Off
    public void PlayFlashLightOn()
    {
        flashLightSource.clip = flashLightOn;
        flashLightSource.Play();
    }

    public void PlayFlashLightOff()
    {
        flashLightSource.clip = flashLightOff;
        flashLightSource.Play();
    }
    #endregion
}
