using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

[Serializable]
public class DragEvent : UnityEvent<float> { }

public class DragInteractable : XRBaseInteractable
{
    [SerializeField] Transform startPos = null;
    [SerializeField] Transform endPos = null;

    [HideInInspector]
    public float dragPercent = 0.0f; //[0,1]

    protected XRBaseInteractor m_interactor = null;

    public UnityEvent onDragStart = new UnityEvent();
    public UnityEvent onDragEnd = new UnityEvent();
    public DragEvent onDragUpdate = new DragEvent();

    Coroutine m_drag = null;

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        m_interactor = interactor;
        StartDrag();
        base.OnSelectEntered(interactor);
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        EndDrag();
        m_interactor = null;
        base.OnSelectExited(interactor);
    }

    private void StartDrag()
    { 
        if (m_drag != null)
        {
            StopCoroutine(m_drag);
        }

        m_drag = StartCoroutine(CalculateDrag());
        onDragStart?.Invoke();
    }

    private void EndDrag()
    {
        if (m_drag != null)
        {
            StopCoroutine(m_drag);
            m_drag = null;
        }
        onDragEnd?.Invoke();
    }

    private IEnumerator CalculateDrag()
    {
        while (m_interactor != null)
        {
            // get a line in local space

            Vector3 line = startPos.localPosition - endPos.localPosition;

            // convert our interactor pos to local space
            Vector3 interactorLocalPos = startPos.parent.InverseTransformPoint(m_interactor.transform.position);

            // project interactor pos onto the line
            Vector3 projectedPoint = Vector3.Project(interactorLocalPos, line.normalized);

            // reverse interpolate that positon on the line to get a % of how far the drag has moved
            dragPercent = InverseLerp(startPos.localPosition, endPos.localPosition, projectedPoint);

            onDragUpdate?.Invoke(dragPercent);

            yield return null;
        }
    }

        public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
        {
            Vector3 AB = b - a;
            Vector3 AV = value - a;

            return Mathf.Clamp01(Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB));
        }
 }
