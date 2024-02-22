using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool isPaused;
    public TextMeshProUGUI textPauseButton;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = true;
        textPauseButton.text = "Start";
        Time.timeScale = 0;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void OnButtonPress()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        textPauseButton.text = isPaused ? "Start" : "Pause";
    }
}
