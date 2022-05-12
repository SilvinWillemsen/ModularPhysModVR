using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tilia;

public class CustomGrabInteraction : MonoBehaviour
{
    [SerializeField] private float distFromPlayerFixed = 1.5f;
    [SerializeField] private float distFromPlayerFollowed= 0.2f;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float instrumentHoldHeight = 1.0f;
    [SerializeField] private float fixedYOffset = 0.0f;

    [SerializeField] private GameObject stage;
    [SerializeField] public bool moveToStage;

    private float currentTime;
    private float transitionTime; 
    private Vector3 playerPos;
    private Vector3 startPos;

    private GameObject thisGameObject;
    private GameObject thisGameObjectParent;

    private bool holdOn = false;

    private Quaternion newOrientation;
    private GameObject instrumentChild;


    // Start is called before the first frame update
   /* void Start()
    {

        //grabbedObjectExtractorLeft = distanceGrabberExtractorLeft.GetComponent<Tilia.Interactions.Interactables.Interactables.Operation.Extraction.InteractableFacadeExtractor>();
        //grabbedObjectExtractorRight = distanceGrabberExtractorRight.GetComponent<Tilia.Interactions.Interactables.Interactables.Operation.Extraction.InteractableFacadeExtractor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(holdOn)
        {
            currentTime += Time.deltaTime; 
            if(currentTime >= transitionTime)
            {
                Vector3 playerFrontPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, distFromPlayerFollowed));
                Vector3 movePos = new Vector3(playerFrontPos.x, instrumentHoldHeight, playerFrontPos.z);
                thisGameObject.transform.position = movePos;
                Vector3 camRot = Camera.main.transform.rotation.eulerAngles;
                thisGameObject.transform.rotation = Quaternion.Euler(thisGameObject.transform.rotation.x, camRot.y, thisGameObject.transform.rotation.z);
               
            }
            
        }
    }

    public void CustomGrab(Tilia.Interactions.Interactables.Interactables.Operation.Extraction.InteractableFacadeExtractor grabbedObjectExtractor)
    {

        thisGameObject = grabbedObjectExtractor.Source.gameObject;
        instrumentChild = thisGameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;

        // despawn instrument when grabbed
       
       

        *//*// check if instrument is grabbed and not exciter
        if (instrumentChild.GetComponent<CustomGrabAttachment>() != null)
        {
            //DespawnCheck(instrumentChild);
            if (instrumentChild.GetComponent<CustomGrabAttachment>().grabAndFollow)
            {
                GrabAndFollow();
            }
            else
            {
                GrabFixed();
            }
        }*//*
    }

    void GrabFixed()
    {
        //thisGameObject = grabbedObjectExtractor.Source.gameObject;
        if (!instrumentChild.GetComponent<CustomGrabAttachment>().isGrabbed)
        {
            instrumentChild.GetComponent<CustomGrabAttachment>().isGrabbed = true;
            thisGameObjectParent = thisGameObject.transform.parent.gameObject;
            thisGameObject.GetComponent<Rigidbody>().isKinematic = true;
            startPos = thisGameObject.transform.position;
            MoveToFixedPosition();
        }
        else
        {
            instrumentChild.GetComponent<CustomGrabAttachment>().isGrabbed = false; 
            GetComponent<ResetInstrumentPos>().DespawnAndSpawnInstrument(thisGameObject.transform.parent.gameObject);
        }
    }

    void GrabAndFollow()
    {
        //thisGameObject = grabbedObjectExtractor.Source.gameObject;
        if (!instrumentChild.GetComponent<CustomGrabAttachment>().isGrabbed)
        {
            instrumentChild.GetComponent<CustomGrabAttachment>().isGrabbed = true;
            thisGameObjectParent = thisGameObject.transform.parent.gameObject;

            //Quaternion objectRot = thisGameObject.transform.rotation;
            //newOrientation = new Quaternions
            thisGameObject.GetComponent<Rigidbody>().isKinematic = true;
            startPos = thisGameObject.transform.position;
            MoveToAndStartFollowing();
        }
        else
        {
            holdOn = false; 
            thisGameObject.GetComponent<Rigidbody>().isKinematic = false;
            instrumentChild.GetComponent<CustomGrabAttachment>().isGrabbed = false;
            GetComponent<ResetInstrumentPos>().DespawnAndSpawnInstrument(thisGameObject.transform.parent.gameObject);
        }
    }

    void MoveToFixedPosition()
    {
        Vector3 playerFrontPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, distFromPlayerFixed));
        Vector3 movePos;
        if (moveToStage)
            movePos = new Vector3(stage.transform.position.x, stage.transform.position.y + thisGameObject.transform.position.y + fixedYOffset, stage.transform.position.z);
        else
            movePos = new Vector3(playerFrontPos.x, thisGameObject.transform.position.y + fixedYOffset, playerFrontPos.z);

        float distance = Vector3.Distance(movePos, thisGameObject.transform.position);
        transitionTime = distance / moveSpeed; // ensure transition always goes with the same speed

        iTween.MoveTo(thisGameObject, movePos, transitionTime);
        if (moveToStage)
        {
            Quaternion rot = stage.transform.rotation;
            iTween.RotateTo(thisGameObject, rot.eulerAngles, transitionTime);
        }
        //objectToMove.GetComponent<Rigidbody>().isKinematic = false;
    }

    void MoveToAndStartFollowing()
    {
        Vector3 playerFrontPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, distFromPlayerFollowed));
        Vector3 movePos = new Vector3(playerFrontPos.x, instrumentHoldHeight, playerFrontPos.z);
        float distance = Vector3.Distance(movePos, thisGameObject.transform.position);
        transitionTime = distance / moveSpeed; // ensure transition always goes with the same speed

        iTween.MoveTo(thisGameObject, movePos, transitionTime);

        currentTime = 0;
        holdOn = true;
        Quaternion rot = Camera.main.transform.rotation;
        iTween.RotateTo(thisGameObject, rot.eulerAngles, transitionTime);
    }

    void DespawnCheck(GameObject instrumentToExclude)
    {
        foreach (GameObject child in GetComponent<InstrumentReferenceList>().instruments)
        {
            foreach(Transform childTransf in child.transform)
            {
                if (childTransf.tag == "Instrument")
                {
                    GameObject instrumentChild = childTransf.GetChild(0).gameObject.transform.GetChild(1).gameObject;
                    if (instrumentChild.GetComponent<CustomGrabAttachment>().isGrabbed && instrumentChild != instrumentToExclude)
                    {
                        holdOn = false;
                        childTransf.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        instrumentChild.GetComponent<CustomGrabAttachment>().isGrabbed = false;
                        GetComponent<ResetInstrumentPos>().DespawnAndSpawnInstrument(child);
                    }
                }
            }
        }
    }*/

}
