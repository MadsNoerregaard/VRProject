using UnityEngine;
using System.Collections;

public class BlackoutToggle : MonoBehaviour
{
    public GameObject blackScreenCanvas;
    public float fadeDuration; // Duration for the fade effect

    private CanvasGroup canvasGroup;
    private bool isBlackScreenActive = false;
    private Coroutine fadeCoroutine;

    void Start()
    {
        canvasGroup = blackScreenCanvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component not found on the blackScreenCanvas.");
            return;
        }

        // Ensure the black screen is initially disabled
        canvasGroup.alpha = 0f;
        blackScreenCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isBlackScreenActive = !isBlackScreenActive;
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeBlackScreen(isBlackScreenActive));
        }
    }

    private IEnumerator FadeBlackScreen(bool fadeIn)
    {
        float startAlpha = canvasGroup.alpha;
        float endAlpha = fadeIn ? 1f : 0f;
        float elapsedTime = 0f;

        if (fadeIn)
        {
            blackScreenCanvas.SetActive(true);
        }

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        if (!fadeIn)
        {
            blackScreenCanvas.SetActive(false);
        }
    }
}
