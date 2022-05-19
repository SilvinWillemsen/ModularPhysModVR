using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tilia;
using Zinnia.Action;

public class MoveInstrumentToStage : MonoBehaviour
{
    //public GameObject[] interactors;

    public Tilia.Interactions.Interactables.Interactables.Operation.Extraction.InteractableFacadeExtractor[] interactorExtractors;

    public GameObject instrumentDisplays;
    Vector3 stagePos;
    GameObject currentInstrument;
    public GameObject distanceGrabber;

    private void Start()
    {
        stagePos = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
    }
    public void MoveInstrument(GameObject instrument)
    {

        if (currentInstrument == null)
        {
            currentInstrument = instrument;
            foreach (Transform child in instrument.transform)
            {
                if (child.tag == "Instrument")
                {
                    child.transform.GetChild(0).transform.GetChild(1).GetComponent<CustomGrabAttachment>().moveToStageWhenGrabbed = true;
                }
            }
            Vector3 moveSpot = new Vector3(stagePos.x, stagePos.y + instrument.transform.position.y, stagePos.z);
            StartCoroutine(DelayBeforeUngrab(currentInstrument));
        }
        else
        {
            currentInstrument = null;
            foreach (Transform child in instrument.transform)
            {
                if (child.tag == "Instrument")
                {
                    child.transform.GetChild(0).transform.GetChild(1).GetComponent<CustomGrabAttachment>().moveToStageWhenGrabbed = false;
                }
            }
            Vector3 moveSpot = new Vector3(stagePos.x, stagePos.y + instrument.transform.position.y, stagePos.z);
            StartCoroutine(DelayBeforeUngrab(currentInstrument));


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

        // Get the instrument to despawn
        for(int i = 0; i < interactorExtractors.Length; i ++)
        {
            //Debug.Log(interactors[i].GetComponent<Tilia.Interactions.PointerInteractors.DistanceGrabberFacade>().CurrentInteractable.gameObject);
            if (interactorExtractors[i].Source != null)
            {
                GameObject grabbedObject = interactorExtractors[i].Source.gameObject;
                if (grabbedObject.tag == "Instrument")
                {
                    grabbedObject.GetComponent<Tilia.Interactions.Interactables.Interactables.InteractableFacade>().Ungrab();
                }
            }
            /*if (interactors[i].GetComponent<Tilia.Interactions.PointerInteractors.DistanceGrabberFacade>().CurrentInteractable.gameObject != null)
            {
                GameObject grabbedObject = interactors[i].GetComponent<Tilia.Interactions.PointerInteractors.DistanceGrabberFacade>().CurrentInteractable.gameObject;
                if (grabbedObject.tag == "Instrument")
                {
                    interactors[i].GetComponent<Tilia.Interactions.Interactables.Interactors.InteractorFacade>().Ungrab();
                }
            }*/

  
        }
        


       /* interactorLeft.GetComponent<Tilia.Interactions.Interactables.Interactors.InteractorFacade>().Ungrab();
        interactorRight.GetComponent<Tilia.Interactions.Interactables.Interactors.InteractorFacade>().Ungrab();
*/
        // GameObject instrumentChild = instrument.transform.GetChild(0).gameObject;
        // instrument.GetComponent<Tilia.Interactions.Interactables.Interactables.InteractableFacade>().Ungrab(instrument);
        //yield return new WaitForSeconds(0.1f);
        //instrumentChild.transform.position = moveSpot;

    }

    // All of the following just used for testing.. Not working!
    public void BeforeGrabbedTest(GameObject harp)
    {
        Debug.Log("Before Grabbed");
        Debug.Log(harp.GetComponent<Rigidbody>());
        Debug.Log("Current interactable: " + distanceGrabber.GetComponent<Tilia.Interactions.PointerInteractors.DistanceGrabberFacade>());


        //distanceGrabber.GetComponent<Tilia.Interactions.PointerInteractors.DistanceGrabberFacade>().GrabCanceled.Invoke(harp.GetComponent<Tilia.Interactions.Interactables.Interactables.InteractableFacade>());
        Debug.Log("Invoke cancelgrab");

        //interactor.GetComponent<Tilia.Interactions.Interactables.Interactors.InteractorFacade>().GrabAction.GetComponent<BooleanAction>().Deactivated.Invoke(false);
        //interactor.GetComponent<Tilia.Interactions.Interactables.Interactors.InteractorFacade>().Ungrab();

    }
    public void AfterGrabbedTest()
    {
        //Debug.Log("Current interactable: " + distanceGrabber.GetComponent<Tilia.Interactions.PointerInteractors.DistanceGrabberFacade>().CurrentInteractable);
        Debug.Log("CancelInvoke");
    }
}