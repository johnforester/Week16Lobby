using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;


public class TurnInteractable : XRBaseInteractable
{
    protected XRBaseInteractor m_interactor = null;
    Coroutine m_turn = null;

    [HideInInspector]
    public float turnAngle = 0f;

    Vector3 m_startingRotation = Vector3.zero;

    public UnityEvent onTurnStart = new UnityEvent();
    public UnityEvent onTurnEnd = new UnityEvent();
    public UnityEvent<float, int> onTurnUpdate = new UnityEvent<float, int>();

    [SerializeField] int note = 0;

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        m_interactor = interactor;
        StartTurn();
        base.OnSelectEntered(interactor);
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        StopTurn();
        m_interactor = null;
        base.OnSelectExited(interactor);
    }

    void StartTurn()
    {
        if (m_turn != null)
        {
            StopCoroutine(m_turn);
        }

        Quaternion localRotation = GetLocalRotation(m_interactor.transform.rotation);
        m_startingRotation = localRotation.eulerAngles;
        m_turn = StartCoroutine(UpdateTurn());
        onTurnStart?.Invoke();
    }

    void StopTurn()
    {
        if (m_turn != null)
        {
            StopCoroutine(m_turn);
            onTurnEnd?.Invoke();
            m_turn = null;
        }
    }

    Quaternion GetLocalRotation(Quaternion targetWorldRotation)
    {
        return Quaternion.Inverse(targetWorldRotation) * transform.rotation;
    }

    IEnumerator UpdateTurn()
    {
        while(m_interactor != null)
        {
            Quaternion localRotation = GetLocalRotation(m_interactor.transform.rotation);
            turnAngle = m_startingRotation.z - localRotation.eulerAngles.z;
            onTurnUpdate?.Invoke(turnAngle, note);

            yield return null;

        }
    }
}
