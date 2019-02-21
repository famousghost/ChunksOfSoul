using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActive : MonoBehaviour {

    #region Audio Source
    [SerializeField]
    private AudioSource audioSoruce;
    #endregion

    #region Audio Clip
    [SerializeField]
    private AudioClip switchClip;
    #endregion

    #region System Methods
    // Use this for initialization
    void Start () {
        switchClip = Resources.Load("Switches/Switch", typeof(AudioClip)) as AudioClip;
        audioSoruce = GetComponent<AudioSource>();
        audioSoruce.clip = switchClip;
	}
    #endregion

    #region PlaySound
    public void PlayPowerOnSound()
    {
        audioSoruce.Play();
    }
    #endregion
}
