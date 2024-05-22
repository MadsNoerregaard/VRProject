using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggerLight : MonoBehaviour
{
    public Experiment experiment;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Hand")) {
            experiment.OnLighterButtonClick(); 
        }
    }
}
