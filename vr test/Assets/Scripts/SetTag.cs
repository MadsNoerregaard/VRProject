using UnityEngine;

public class SetTagRecursively : MonoBehaviour
{
    public string tagToSet = "Hand";

    void Start()
    {
        gameObject.tag = tagToSet;


        SetTagInChildren(transform, tagToSet);
    }

    void SetTagInChildren(Transform parent, string tag)
    {

        parent.gameObject.tag = tag;


        foreach (Transform child in parent)
        {
            SetTagInChildren(child, tag);
        }
    }
}