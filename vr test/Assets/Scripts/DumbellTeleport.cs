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
    public Vector3 positionOffset = new Vector3(0f, -0.1f, 0.2f); 
    public Vector3 rotationOffset = Vector3.zero;
    public Transform centerOfCross;

    public float attachRange = 0.5f;

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
        if (dumbbell != null) {
            grabPointTransform = dumbbell.Find("Grab Point");
        }
        if (dumbbell != null) {
            dumbbellRigidbody = dumbbell.GetComponent<Rigidbody>();
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
                Attach();
            }
            else
            {
                Detach();
            }
        }

        if (Vector3.Distance(handTransform.position, centerOfCross.position) < attachRange)
        {
            if (!isHeld)
            {
                Attach();
            }
        }

    }
    public void Attach() {
        dumbbellRigidbody.isKinematic = true;
        dumbbell.position = handTransform.position + handTransform.TransformDirection(positionOffset);
        dumbbell.rotation = handTransform.rotation * Quaternion.Euler(rotationOffset);
        dumbbell.parent = handTransform;
        handScript.enabled = false;
        handVisual.SetActive(false);
        isHeld = true;
    }
    public void Detach() {
        dumbbell.parent = null;
        dumbbellRigidbody.isKinematic = false;
        handScript.enabled = true;
        handVisual.SetActive(true);
        isHeld = false;
    }
}
