using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {

    [SerializeField]
    private string tutorialText;

    [SerializeField]
    private TutorialDisplay tutorialDisplay;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            tutorialDisplay = other.GetComponent<TutorialDisplay>();
            tutorialDisplay.SetTutorialText(tutorialText);
            tutorialDisplay.EnteredTrigger(true);
            tutorialDisplay.SetTimer(0.4f);
            Destroy(this);
        }
    }
}
