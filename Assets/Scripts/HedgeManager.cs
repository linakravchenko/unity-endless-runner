using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class HedgeManager : MonoBehaviour
{

    public GameObject[] hedgePrefabs;

    private Transform playerTransform;
    private float spawnZ = 5.0f;
    private float spawnX = -8.52f;
    private float distance;
    private float safeZone = 20.0f;
    private int enemiesOnScreen = 4;
    private int hedgeIndex, lastPrefabIndex, index;

    private List<GameObject> activeHedges;

    void Start()
    {
        activeHedges = new List<GameObject>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < enemiesOnScreen; i++)
        {
            index = RandomPrefabIndex();
            SpawnHedge(index);
        }
    }

    void Update()
    {
        if (playerTransform.position.z + safeZone > (spawnZ))
        {
            index = RandomPrefabIndex();
            SpawnHedge(index);
        }
        if (activeHedges[0].transform.position.z < playerTransform.position.z - Screen.resolutions[0].width / 10)
            DeleteHedge();
    }

    private void SpawnHedge(int prefabIndex)
    {
        GameObject go1;
        go1 = Instantiate(hedgePrefabs[prefabIndex]) as GameObject;
        go1.transform.SetParent(transform);

        distance = RandomDistance();
        spawnZ += distance + go1.GetComponent<Collider>().bounds.size.z;

        go1.transform.position = new Vector3(spawnX, 3.0f, spawnZ);

        activeHedges.Add(go1);
    }

    private void DeleteHedge()
    {
        Destroy(activeHedges[0]);
        activeHedges.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        /*if (hedgePrefabs.Length <= 1)
            return 0;
        //while (randomIndex == lastPrefabIndex)
            randomIndex = Random.Range(0, hedgePrefabs.Length);

        lastPrefabIndex = randomIndex;*/

        if (Random.value <= 0.1) //%10 chance
        {
            for (int i = 0; i < hedgePrefabs.Length; i++)
            {
                if (hedgePrefabs[i].tag == "Heart")
                {
                    if (i != lastPrefabIndex)
                    {
                        hedgeIndex = i;
                        lastPrefabIndex = hedgeIndex;
                        return i;
                    }
                }
            }
        }
        else if (Random.value <= 0.2) //%20 chance
        {
            for (int i = 0; i < hedgePrefabs.Length; i++)
            {
                if (hedgePrefabs[i].tag == "Trash")
                {
                    if (i != lastPrefabIndex)
                    {
                        hedgeIndex = i;
                        lastPrefabIndex = hedgeIndex;
                        return i;
                    }
                }
            }
        }
        else if (Random.value <= 0.8) //%80 chance
        {
            hedgeIndex = Random.Range(0, 4);
            lastPrefabIndex = hedgeIndex;
            return hedgeIndex;
        }

        hedgeIndex = Random.Range(0, 4);
        lastPrefabIndex = hedgeIndex;

        return hedgeIndex;
    }

    private int RandomDistance()
    {
        int randDistance = Random.Range(10, 35);
        return randDistance;
    }
}
