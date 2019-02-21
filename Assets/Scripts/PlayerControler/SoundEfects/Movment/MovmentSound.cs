using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class MovmentSound : MonoBehaviour {

    #region AudioSource
    [SerializeField]
    private AudioSource audioSource;
    #endregion

    #region Timers
    private float waitingForFootstep = 0.0f;

    private const float TIMETOFOOTSTEP = 0.5f;

    private const float RAYDISTANCE = 2.0f;
    #endregion

    #region AudioClip
    [SerializeField]
    private AudioClip walkingSound;

    [SerializeField]
    private AudioClip jumpSound;
    #endregion

    #region System Methods
    // Use this for initialization
    void Awake () {
        waitingForFootstep = TIMETOFOOTSTEP;
        audioSource = GetComponent<AudioSource>();
        walkingSound = Resources.Load("GameSounds/Movement/SpaceShipWalk", typeof(AudioClip)) as AudioClip;
        jumpSound = Resources.Load("GameSounds/Movement/Jump", typeof(AudioClip)) as AudioClip;
    }

    void Update()
    {
        CheckWhichSurfacePlayerIsWalking();
    }
    #endregion

    #region Walk Sound Methods
    public void PlayWalkSound()
    {
        if (audioSource.clip != null)
        {
            if (waitingForFootstep >= 0.0f)
            {
                waitingForFootstep -= Time.deltaTime;
            }
            else
            {
                waitingForFootstep = TIMETOFOOTSTEP;
                audioSource.Play();
            }
        }
    }
    #endregion

    #region Run Sound Methods
    public void PlayRunSound()
    {
        if (audioSource.clip != null)
        {
            if (waitingForFootstep >= 0.0f)
            {
                waitingForFootstep -= Time.deltaTime * 1.3f;
            }
            else
            {
                waitingForFootstep = TIMETOFOOTSTEP;
                audioSource.Play();
            }
        }
    }
    #endregion

    #region Stop Sound
    public void StopSound()
    {
        if (audioSource.clip != null && audioSource.clip != jumpSound)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
    #endregion

    #region GetterToAudioClip

    public AudioClip GetJumpSound()
    {
        return jumpSound;
    }
    #endregion

    #region RayCast to check on which surface player is walking to change sound

    private void CheckWhichSurfacePlayerIsWalking()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, RAYDISTANCE))
        {
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Corridors"))
            {
                walkingSound = Resources.Load("GameSounds/Movement/SpaceShipWalk", typeof(AudioClip)) as AudioClip;
                audioSource.clip = walkingSound;
            }
            else if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Rooms"))
            {
                walkingSound = Resources.Load("GameSounds/Movement/RoomFootsteps", typeof(AudioClip)) as AudioClip;
                audioSource.clip = walkingSound;
            }
            else
            {
                walkingSound = null;
                audioSource.clip = null;
            }
        }
    }
    #endregion
}
