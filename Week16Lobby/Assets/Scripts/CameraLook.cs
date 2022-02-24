using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    public Camera playerCamera;

    private float deltaX;
    private float deltaY;

    private float xRotation;
    private float yRotation;

    [SerializeField] float mouseSensitivity = 10f;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        yRotation += deltaX * mouseSensitivity * Time.deltaTime;
        xRotation += deltaY * mouseSensitivity * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void OnCameraLook(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        deltaX = inputVector.x;
        deltaY = inputVector.y;
    }
}
