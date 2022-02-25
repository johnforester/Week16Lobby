using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class DrumMachineUIRow : MonoBehaviour
{
    [SerializeField] Sequencer sequencer;
    [SerializeField] int noteNumber;

    public void ToggleNote(bool isOn, int beat)
    {
        if (isOn)
        {
            sequencer.AddNote(noteNumber, beat, beat + 2, 1.0f);
        }
        else
        {
            sequencer.RemoveNotesInRange(noteNumber, beat, beat + 2);
        }
    }
}
