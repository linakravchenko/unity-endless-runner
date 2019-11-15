using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetManager : MonoBehaviour
{

    public GameObject[] streetPrefabs;

    private Transform playerTransform;
    private float spawnZ = -15.0f;
    private float streetLenght;
    private float safeZone = 40.0f;
    private int streetsFirst = 16;
    private int lastPrefabIndex, index;

    private List<GameObject> activeStreets;

    void Start()
    {
        activeStreets = new List<GameObject>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < streetsFirst; i++)
        {
            index = RandomPrefabIndex();
            SpawnStreet(index);
        }
    }

    void Update()
    {
        if (playerTransform.position.z + safeZone > (spawnZ))
        {
            index = RandomPrefabIndex();
            SpawnStreet(index);
        }
        if (activeStreets[0].transform.position.z < playerTransform.position.z - Screen.resolutions[0].width / 10)
            DeleteStreet();
    }

    private void SpawnStreet(int prefabIndex)
    {
        GameObject go1;
        go1 = Instantiate(streetPrefabs[prefabIndex]) as GameObject;
        go1.transform.SetParent(transform);

        spawnZ += streetLenght + go1.GetComponent<Collider>().bounds.size.z / 2;
        streetLenght = go1.GetComponent<Collider>().bounds.size.z / 2;

        go1.transform.position = Vector3.forward * spawnZ;
        activeStreets.Add(go1);
    }

    private void DeleteStreet()
    {
        Destroy(activeStreets[0]);
        activeStreets.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (streetPrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;

        while (randomIndex == lastPrefabIndex)
            randomIndex = Random.Range(0, streetPrefabs.Length);

        lastPrefabIndex = randomIndex;
  
        return randomIndex;
    }
}
