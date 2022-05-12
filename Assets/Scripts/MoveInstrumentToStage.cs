using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tilia; 


public class MoveInstrumentToStage : MonoBehaviour
{
    public GameObject interactor;
    public GameObject instrumentDisplays;
    Vector3 stagePos;

    GameObject currentInstrument; 


    private void Start()
    {
        stagePos = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
    }
    public void MoveInstrument(GameObject instrument)
    {
        
        if(currentInstrument == null)
        {
            currentInstrument = instrument;
            StartCoroutine(DelayBeforeUngrab(currentInstrument));
        }
        else
        {
            currentInstrument = null; 
            
            //instrumentDisplays.GetComponent<ResetInstrumentPos>().DespawnAndSpawnInstrument(instrument);
        }

      

        
        /*foreach(Transform child in instrument.transform)
        {
            if(child.tag == "Instrument")
            {

                instrument.transform.position = transform.position;
            }
        }*/
    }

    IEnumerator DelayBeforeUngrab(GameObject instrument)
    {

        yield return new WaitForSeconds(0.1f);
       
        interactor.GetComponent<Tilia.Interactions.Interactables.Interactors.InteractorFacade>().Ungrab();
        GameObject instrumentChild = instrument.transform.GetChild(0).gameObject;
        instrumentChild.GetComponent<Tilia.Interactions.Interactables.Interactables.InteractableFacade>().Ungrab(instrumentChild);

        instrumentChild.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(0.1f);
        instrumentChild.transform.position = stagePos;
        
    }

    
}
