using UnityEngine;

public class WeightManager : MonoBehaviour
{
    public GameObject weightType1Prefab;
    public GameObject weightType2Prefab;
    public GameObject weightType3Prefab;
    public Vector3 spawnPosition;

    void Start()
    {
        if (weightType1Prefab != null)
            spawnPosition = weightType1Prefab.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnWeight(weightType1Prefab);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnWeight(weightType2Prefab);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpawnWeight(weightType3Prefab);
        }
    }

    void SpawnWeight(GameObject weightPrefab)
    {
        DestroyExistingWeights();
        Instantiate(weightPrefab, spawnPosition, Quaternion.identity);
    }

    void DestroyExistingWeights()
    {
        foreach (var existingWeight in FindObjectsOfType<GameObject>())
        {
            if (existingWeight.name.Contains("WeightType"))
            {
                Destroy(existingWeight);
            }
        }
    }
}