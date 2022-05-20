using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelGrab : MonoBehaviour
{
    public GameObject instrumentDisplays;
    public GameObject exciters;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DespawnInteractable()
    {
        GameObject currentInteractable = GetComponent<Tilia.Interactions.PointerInteractors.DistanceGrabberFacade>().CurrentInteractable.gameObject;
        Debug.Log("Despawn Grab-Cancelled Interactable: " + currentInteractable.tag);
        if (currentInteractable.tag == "Instrument")
        {
            StartCoroutine(instrumentDisplays.GetComponent<ResetInstrumentPos>().StartResetCoroutine (currentInteractable.transform.parent.gameObject, 
            instrumentDisplays.GetComponent<ResetInstrumentPos>().timeBeforeDespawn, 
            instrumentDisplays.GetComponent<ResetInstrumentPos>().transitionTime, false));
        } 
        else if (currentInteractable.tag == "Exciter")
        {
            StartCoroutine(exciters.GetComponent<ResetExciterPos>().StartResetCoroutine (currentInteractable.transform.parent.gameObject, 
            exciters.GetComponent<ResetExciterPos>().despawnTime, 
            exciters.GetComponent<ResetExciterPos>().spawnTime, 
            exciters.GetComponent<ResetExciterPos>().transitionTime));
        } else {
            Debug.Log("All interactables should either be an instrument or exciter!");
        }
    }
}
