using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckWin : MonoBehaviour
{
    [SerializeField] GameObject winMessage;

    public UnityEvent onWin = new UnityEvent();

    void Start()
    {
        winMessage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        onWin?.Invoke();

        if (other.gameObject.CompareTag("MainCamera") && !winMessage.activeInHierarchy)
        {
            winMessage.SetActive(true);
        }
    }
}
