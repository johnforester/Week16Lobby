using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dial : MonoBehaviour
{
    Vector3 m_startRotation;

    MeshRenderer m_renderer = null;

    [SerializeField] TMP_Text volText;

    void Start()
    {
        m_renderer = GetComponent<MeshRenderer>();
    }

    public void StartTurn()
    {
        m_startRotation = transform.localEulerAngles;
        m_renderer.material.SetColor("_Color", Color.red);
    }

    public void StopTurn()
    {
        m_renderer.material.SetColor("_Color", Color.white);
    }

    public void DialUpdate(float angle, int note)
    {
        Vector3 angles = m_startRotation;
        angles.y += angle;
        transform.localEulerAngles = angles;

        float volume = (1f / 360f) * Mathf.Abs(angle) * 100;  //(0-100 velocity
        volText.text = "Vol: " + (int)volume;
    }
}
