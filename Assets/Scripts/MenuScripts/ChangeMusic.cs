using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour {

    #region AudioSource
    [SerializeField]
    private AudioSource backGroundMusic;
    #endregion

    #region Float
    [SerializeField]
    private float noramlPitch = 1.0f;


    [SerializeField]
    private float timerToJumpScare = 130.0f;
    #endregion

    #region System Methods
    // Use this for initialization
    void Start () {
        backGroundMusic = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        ChangePitch();
    }
    #endregion

    #region Change Pitch
    private void ChangePitch()
    {
        timerToJumpScare -= 0.05f;
        if(timerToJumpScare<=0.0f)
            backGroundMusic.pitch += 0.01f;

        if (backGroundMusic.pitch >= 2.0f)
        {
            backGroundMusic.pitch = noramlPitch;
            timerToJumpScare = Random.Range(50.0f, 200.0f);
        }
    }
    #endregion
}
