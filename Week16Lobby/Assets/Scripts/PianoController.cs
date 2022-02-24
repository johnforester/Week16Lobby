using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoController : MonoBehaviour
{
    private PlayerBehavior thePlayerBehavior;

    private void OnEnable()
    {
        if (thePlayerBehavior == null)
        {
            thePlayerBehavior = FindObjectOfType<PlayerBehavior>();
        }

        Cursor.visible = true;
        thePlayerBehavior.ToggleCameraLook(false);
    }

    private void OnDisable()
    {
        Cursor.visible = false;
        thePlayerBehavior.ToggleCameraLook(true);
    }
}
