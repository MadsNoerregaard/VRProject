using Oculus.Interaction.Input;
using UnityEngine;

public class TeleportDumbbell : MonoBehaviour
{
    private Transform dumbbell;
    public OVRCameraRig cameraRig;
    private Transform handTransform;
    private Rigidbody dumbbellRigidbody;
    private Transform grabPointTransform;
    private bool isHeld = false;
    private string handPath = "TrackingSpace/RightHandAnchor";
    public GameObject hand;
    public GameObject handVisual;
    private Hand handScript;

    // Add fields for offset
    public Vector3 positionOffset = new Vector3(0f, -0.1f, 0.2f); // Adjust this value based on your needs
    public Vector3 rotationOffset = Vector3.zero; // Additional field for rotation offset

    void Start()
    {
        handScript = hand.GetComponent<Hand>();
    }

    void Update()
    {
        GameObject[] weights = GameObject.FindGameObjectsWithTag("Weight");
        if (weights.Length > 0)
        {
            dumbbell = weights[0].transform;  
        }

        grabPointTransform = dumbbell.Find("Grab Point");
        if (grabPointTransform == null)
        {
            Debug.LogError("Grab Point not found in the dumbbell prefab!");
            return;
        }

        dumbbellRigidbody = dumbbell.GetComponent<Rigidbody>();
        if (dumbbellRigidbody == null)
        {
            Debug.LogError("Rigidbody not found on the dumbbell!");
            return;
        }

        handTransform = cameraRig.transform.Find(handPath);

        if (handTransform == null)
        {
            Debug.LogError("Hand transform not found at path: " + handPath);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isHeld)
            {
                dumbbellRigidbody.isKinematic = true;

                dumbbell.position = handTransform.position + handTransform.TransformDirection(positionOffset);
                dumbbell.rotation = handTransform.rotation * Quaternion.Euler(rotationOffset);
                dumbbell.parent = handTransform;
                handScript.enabled = false;
                handVisual.SetActive(false);
                isHeld = true;
            }
            else
            {
                dumbbell.parent = null;
                dumbbellRigidbody.isKinematic = false;
                handScript.enabled = true;
                handVisual.SetActive(true);
                isHeld = false;
            }
        }
    }
}
