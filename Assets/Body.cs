using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public GameObject[] bodyPart;

    // Update is called once per frame
    public void disableBodyParts()
    {
        foreach(GameObject obj in bodyPart)
        {
            obj.SetActive(false);
        }
    }

    public void enableBodyParts()
    {
        foreach (GameObject obj in bodyPart)
        {
            obj.SetActive(true);
        }
    }
}
