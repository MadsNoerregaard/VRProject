using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlackoutToggle : MonoBehaviour
{
    public GameObject blackScreenCanvas;
    public Image blackScreenImage;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = blackScreenCanvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component not found on the blackScreenCanvas.");
            return;
        }
        canvasGroup.alpha = 1f;
        blackScreenCanvas.SetActive(false);
    }

    void Update()
    {
        
    }
    public void StopBlackOut(){
        blackScreenCanvas.SetActive(false);
    }
    public void StartBlackOut(){
        blackScreenCanvas.SetActive(true);
    }
    public void EndExperiment(){
        blackScreenImage.color = Color.white;
        blackScreenCanvas.SetActive(true);
    }
}
