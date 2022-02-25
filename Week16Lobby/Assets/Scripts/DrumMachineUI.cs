using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumMachineUI : MonoBehaviour
{
    [SerializeField] List<GameObject> BeatMarkers = new List<GameObject>();


    private int m_currentBeat = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject beatMarker in BeatMarkers)
        {
            beatMarker.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeatChanged(int beat)
    {
        if (beat < 0 || beat >= BeatMarkers.Count)
        {
            return;
        }

        BeatMarkers[m_currentBeat].SetActive(false);
        m_currentBeat = beat;
        BeatMarkers[m_currentBeat].SetActive(true);
    }
}
