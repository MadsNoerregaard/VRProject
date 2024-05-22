using UnityEngine;

public class WeightManager : MonoBehaviour
{
    public GameObject weightType1Prefab;
    public GameObject weightType2Prefab;
    public GameObject weightType3Prefab;
    public Vector3 spawnPosition;

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnWeight(GameObject weightPrefab)
    {
        DestroyExistingWeights();
        Instantiate(weightPrefab, spawnPosition, Quaternion.identity);
    }

    public void DestroyExistingWeights()
    {
        foreach (var existingWeight in FindObjectsOfType<GameObject>())
        {
            if (existingWeight.name.Contains("WeightType"))
            {
                Destroy(existingWeight);
            }
        }
    }
    public void ChooseWeight(string val) {
        if (val == "light")
        {
            SpawnWeight(weightType1Prefab);
        }
        else if (val == "medium")
        {
            SpawnWeight(weightType2Prefab);
        }
        else if (val == "heavy")
        {
            SpawnWeight(weightType3Prefab);
        }
    }
}