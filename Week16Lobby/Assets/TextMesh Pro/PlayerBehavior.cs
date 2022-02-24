using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private CameraLook cameraLook;

    // Start is called before the first frame update
    void Start()
    {
        cameraLook = GetComponent<CameraLook>();
    }

    public void ToggleCameraLook(bool isOn)
    {
        cameraLook.enabled = isOn;
    }
}
