using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlackoutToggle : MonoBehaviour
{
    public GameObject blackScreenCanvas;
    public Image blackScreenImage;
    private CanvasGroup canvasGroup;
    private Color whiteColor = new Color(255f/255f, 245f/255f, 238f/255f);
    private Color redColor = new Color(255f/255f, 99f/255f, 71f/255f);
    private Color greenColor = new Color(144f/255f, 238f/255f, 144f/255f);

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
        blackScreenImage.color = whiteColor;
        blackScreenCanvas.SetActive(true);
    }
    public void StartRedOut(){
        blackScreenImage.color = redColor;
        blackScreenCanvas.SetActive(true);
    }
    public void StartGreenOut(){
        blackScreenImage.color = greenColor;
        blackScreenCanvas.SetActive(true);
    }
}
