using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButtonColor : MonoBehaviour
{
    MeshRenderer m_meshRenderer = null;

    [SerializeField] Color offColor;
    [SerializeField] Color onColor;

    private void Awake()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
    }
    public void ChangeColor(bool isOn, int beat)
    {
        Color color = isOn ? onColor : offColor;
        m_meshRenderer.material.SetColor("_Color", color);
    }

    public void LogInteractionStart()
    {
       // Debug.Log("Interaction started");
    }
    public void LogInteractionEnd()
    {
        //Debug.Log("Interaction ended");
    }
}
