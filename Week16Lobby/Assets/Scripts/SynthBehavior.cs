using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynthBehavior : MonoBehaviour
{
    [SerializeField] GameObject synthController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            synthController.SetActive(true);

            Synth synth = GetComponentInChildren<Synth>();

            if (synth != null)
            {
                GetComponentInChildren<Synth>().LoadInitialPatch();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            synthController.SetActive(false);
        }
    }
}
