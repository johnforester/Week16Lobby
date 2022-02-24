using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class DrumStickBehavior : MonoBehaviour
{
    [SerializeField] int note = 0;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNoteOnEvent(Note inputNote)
    {
        if (inputNote.note == note)
        {
          //  anim.ResetTrigger("DrumHit");
            anim.SetTrigger("DrumHit");
        }
    }
}
