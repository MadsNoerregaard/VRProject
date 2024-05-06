
using UnityEngine;

public class HandGlue : MonoBehaviour
{
    public Transform holdingPosition;  // Assign this to a specific spot on the model where objects should be held

    private GameObject currentlyHeldObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            if (currentlyHeldObject != null)
            {
                currentlyHeldObject.GetComponent<WeightPhysic>().AttachToHand(holdingPosition);
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (currentlyHeldObject != null)
            {
                currentlyHeldObject.GetComponent<WeightPhysic>().DetachFromHand();
                currentlyHeldObject = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weight"))
        {
            currentlyHeldObject = other.gameObject; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Weight"))
        {
            if (currentlyHeldObject == other.gameObject)
            {
                currentlyHeldObject = null;
            }
        }
    }
}
