using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUnlock : MonoBehaviour
{
    [SerializeField] GameObject door = null;
    [SerializeField] GameObject key = null;
    private void Start()
    {
        door.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == key && door.activeInHierarchy)
        {
            door.SetActive(false);
        }
    }
}
