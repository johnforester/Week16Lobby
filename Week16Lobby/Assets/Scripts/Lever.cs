using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Transform startRot = null;
    public Transform endRot = null;

    MeshRenderer meshRenderer = null;

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void OnLeverPullStart()
    {
        meshRenderer.material.SetColor("_Color", Color.red);
    }

    public void OnLeverPullEnd()
    {
        meshRenderer.material.SetColor("_Color", Color.white);
    }

    public void UpdateLever(float percent)
    {
        transform.rotation = Quaternion.Slerp(startRot.rotation, endRot.rotation, percent);
    }
}

