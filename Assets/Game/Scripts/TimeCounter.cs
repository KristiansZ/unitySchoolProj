using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    private float timeElapsed = 0f;
    private TextMeshProUGUI textMeshPro;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        
        if (textMeshPro == null)
        {
            Debug.LogError("No TextMeshProUGUI component found on the GameObject.");
            return;
        }
        
        timeElapsed = 0f;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        UpdateTimeCounterDisplay();
    }

    private void UpdateTimeCounterDisplay()
    {

        int minutes = Mathf.FloorToInt(timeElapsed / 60f);
        int seconds = Mathf.FloorToInt(timeElapsed % 60f);
        
        textMeshPro.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}