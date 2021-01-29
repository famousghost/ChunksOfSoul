using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpiritChunks : MonoBehaviour
{
    public GameObject[] objectToSpawnList1;
    public GameObject[] objectToSpawnList2;
    public GameObject[] objectToSpawnList3;
    public GameObject[] objectToSpawnList4;

    // Start is called before the first frame update
    void Start()
    {
        spawnChunks();
    }

    void activeSpiritChunks(GameObject[] array)
    {
        foreach(GameObject obj in array)
        {
            obj.SetActive(true);
        }
    }

    public void spawnChunks()
    {
        foreach(GameObject obj in objectToSpawnList1)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in objectToSpawnList2)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in objectToSpawnList3)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in objectToSpawnList4)
        {
            obj.SetActive(false);
        }
        int rnd = Random.Range(0, 201);

        if (rnd <= 50)
        {
            activeSpiritChunks(objectToSpawnList1);
        }
        if (rnd > 50 && rnd <= 100)
        {
            activeSpiritChunks(objectToSpawnList2);
        }
        if (rnd > 100 && rnd <= 150)
        {
            activeSpiritChunks(objectToSpawnList3);
        }
        if (rnd > 150 && rnd <= 200)
        {
            activeSpiritChunks(objectToSpawnList4);
        }
    }
}
