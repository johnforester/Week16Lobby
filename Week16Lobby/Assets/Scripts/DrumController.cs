using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class DrumController : MonoBehaviour
{
    private SampleSequencer sequencer = null;

    // Start is called before the first frame update
    void Start()
    {
        sequencer = GetComponent<SampleSequencer>();
    }

    public void UpdateVolume(float volume, int noteNumber)
    {
        List<Note> notes = sequencer.GetAllNotes();

        foreach (Note note in notes)
        {
            if (note.note == noteNumber)
            {
                note.velocity = (1f / 360f) * Mathf.Abs(volume);
            }
        }
    }

   
}
