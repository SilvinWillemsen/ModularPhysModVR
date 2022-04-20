using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrabChangePreset : MonoBehaviour
{
    public void ChangePreset(Tilia.Interactions.Interactables.Interactables.Operation.Extraction.InteractableFacadeExtractor grabbedObjectExtractor)
    {
        GameObject extractedGameObject = grabbedObjectExtractor.Source.gameObject;
        extractedGameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<SelectPreset>().InstrumentGrabbed();
    }
}
