using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggerHeavy : MonoBehaviour
{
    public Experiment experiment;
    private void OnTriggerEnter(Collider other) {
        experiment.OnHeavierButtonClick(); 
    }
}
