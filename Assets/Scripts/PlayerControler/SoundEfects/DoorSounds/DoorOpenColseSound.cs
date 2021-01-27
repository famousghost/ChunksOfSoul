using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenColseSound : MonoBehaviour
{
    #region Door Open Close Class
    [SerializeField]
    private DoorIsOpenedAndClosed doorOpenedAndClosed;
    #endregion

    #region Audio source
    [SerializeField]
    private AudioSource sourceOfDoorSounds;
    #endregion

    #region AudioClip
    [SerializeField]
    private AudioClip doorOpenSound;

    [SerializeField]
    private AudioClip doorCloseSound;
    #endregion

    #region System Methods
    // Use this for initialization
    void Awake()
    {
        doorOpenedAndClosed = GetComponent<DoorIsOpenedAndClosed>();
        sourceOfDoorSounds = GetComponent<AudioSource>();
        doorOpenSound = Resources.Load("Switches/ClickOn", typeof(AudioClip)) as AudioClip;
        doorCloseSound = Resources.Load("Switches/ClickOn", typeof(AudioClip)) as AudioClip;
    }

    void Update()
    {
        doorOpenSound = Resources.Load("GameSounds/DoorSounds/openDoor", typeof(AudioClip)) as AudioClip;
        doorCloseSound = Resources.Load("GameSounds/DoorSounds/openDoor", typeof(AudioClip)) as AudioClip;
    }
    #endregion

    #region Play Sounds
    private void PlayOpenDoorSound()
    {
        if (doorOpenSound != null)
            sourceOfDoorSounds.clip = doorOpenSound;
    }

    private void PlayCloseDoorSound()
    {
        if (doorCloseSound != null)
            sourceOfDoorSounds.clip = doorCloseSound;
    }

    public void PlayClip()
    {
        if(doorOpenedAndClosed.GetDoorIsOpened())
        {
            PlayOpenDoorSound();
        }
        else
        {
            PlayCloseDoorSound();
        }
        sourceOfDoorSounds.Play();
    }
    #endregion
}
