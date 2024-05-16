using UnityEngine;

public class BlackoutToggle : MonoBehaviour
{
    public GameObject blackScreen; 

    private bool isBlackScreenActive = false;

    void Start()
    {
        blackScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isBlackScreenActive = !isBlackScreenActive;
            blackScreen.SetActive(isBlackScreenActive);
        }
    }
}