using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrabChangePreset : MonoBehaviour
{
    public void ChangePreset(Tilia.Interactions.Interactables.Interactables.Operation.Extraction.InteractableFacadeExtractor grabbedObjectExtractor)
    {
        GameObject extractedGameObject = grabbedObjectExtractor.Source.gameObject;
        Transform instrumentChild = extractedGameObject.transform.GetChild(0).transform.GetChild(1);
        if(instrumentChild.GetComponent<SelectPreset>() != null) instrumentChild.GetComponent<SelectPreset>().InstrumentGrabbed();
    }
}
