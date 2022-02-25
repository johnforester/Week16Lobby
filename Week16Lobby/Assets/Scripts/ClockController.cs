using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;
using TMPro;

public class ClockController : MonoBehaviour
{
    private AudioHelmClock clock;
    [SerializeField] float minBPM = 20f;
    [SerializeField] TMP_Text bpmText;
    private void Awake()
    {
        clock = GetComponent<AudioHelmClock>();
    }

    public void UpdateFromSlider(float value)
    {
        clock.bpm = (value * 200) + minBPM;
        bpmText.text = "BPM: " + (int)clock.bpm;
    }
}
