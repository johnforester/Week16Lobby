using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PushButton : MonoBehaviour
{
    public UnityEvent<bool, int> onPressed = new UnityEvent<bool, int>();
    public UnityEvent onInteractionStart = new UnityEvent();
    public UnityEvent onInteractionEnd = new UnityEvent();

    [SerializeField] Transform clickPoint = null;
    [SerializeField] Transform buttonBottom = null;
    [SerializeField] int beat = 0;

    [Min(0.01f)]
    [SerializeField] float returnSpeed = 1f;

    private List<Collider> m_currentColliders = new List<Collider>();
    private XRBaseInteractor m_interactor = null;

    float m_currentPressDepth;
    float m_yMax = 0.0f; //resting position
    float m_yMin = 0.0f; // all the way pressed in

    bool m_isPressing = false;
    bool m_isOn = false;

    private void Start()
    {
        m_yMax = transform.localPosition.y;
        m_yMin = clickPoint.localPosition.y;
    }

    private void Update()
    {
        if (m_interactor != null)
        {
            float newPressHeight = GetPressDepth(m_interactor.transform.position);
            float deltaHeight = m_currentPressDepth - newPressHeight;
            float newPressedPosition = transform.localPosition.y - deltaHeight;

            SetHeight(newPressedPosition);

            if (!m_isPressing && IsPressed())
            {
                //we pressed the button
                m_isPressing = true;
                m_isOn = !m_isOn;

                onPressed?.Invoke(m_isOn, beat);
            }

            m_currentPressDepth = newPressHeight;
        }
        else if(!Mathf.Approximately(transform.localPosition.y, m_yMax))
        {
            float returnHeight = Mathf.MoveTowards(transform.localPosition.y, m_yMax, Time.deltaTime * returnSpeed);
            SetHeight(returnHeight);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        XRBaseInteractor interactor = other.GetComponentInParent<XRBaseInteractor>();

        if (interactor != null && !other.isTrigger)
        {
            m_currentColliders.Add(other);

            if (m_interactor == null)
            {
                if (m_isPressing && IsReset())
                {
                    m_isPressing = false;
                }

                m_interactor = interactor;
               
                m_currentPressDepth = GetPressDepth(m_interactor.transform.position);
                onInteractionStart?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_currentColliders.Contains(other))
        {
            m_currentColliders.Remove(other);
            
            if (m_currentColliders.Count == 0)
            {
                onInteractionEnd?.Invoke();
                EndPress();
            }
        }
    }

    private void EndPress()
    {
        m_currentColliders.Clear();
        m_currentPressDepth = 0.0f;
        m_interactor = null;
    }

    private float GetPressDepth(Vector3 interactorPosition)
    {
        return transform.parent.InverseTransformPoint(interactorPosition).y;
    }

    private void SetHeight(float newHeight)
    {
        Vector3 currentPosition = transform.localPosition;
        currentPosition.y = newHeight;
        currentPosition.y = Mathf.Clamp(currentPosition.y, m_yMin, m_yMax);
        transform.localPosition = currentPosition;
    }
    private bool IsPressed()
    {        
        return buttonBottom.transform.position.y <= clickPoint.transform.position.y;
    }
    private bool IsReset()
    {
        return buttonBottom.transform.position.y >= clickPoint.transform.position.y;
    }
}
