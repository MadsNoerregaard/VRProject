using UnityEngine;

public class WeightPhysic : MonoBehaviour
{
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void AttachToHand(Transform hand)
    {
        rigidBody.isKinematic = true;  
        this.transform.SetParent(hand);  
        this.transform.localPosition = Vector3.zero;  
        this.transform.localRotation = Quaternion.identity;  
    }

    public void DetachFromHand()
    {
        this.transform.SetParent(null);  
        rigidBody.isKinematic = false;  
    }
}
