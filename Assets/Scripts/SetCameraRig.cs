using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tilia.CameraRigs.TrackedAlias;
using Zinnia.Tracking.CameraRig;
using Tilia.Interactions.Interactables.Interactors;
using Tilia.Interactions.Interactables.Interactables;
using Zinnia.Action;
using System.Linq;
using UnityEditor;

[ExecuteInEditMode]
public class SetCameraRig : MonoBehaviour
{
    public enum XRsetup
    {
        UseOculus,
        UseSimulator
    }

    public XRsetup xrSetup;
    private XRsetup curXRSetup;

    public GameObject TrackedAliasObj;
    public LinkedAliasAssociationCollection OculusCameraRig;
    public LinkedAliasAssociationCollection SimulatorCameraRig;
    
    [SerializeField]
    public GameObject LeftInteractorObj;
    [SerializeField]
    public GameObject RightInteractorObj;

    public BooleanAction OculusLeftGrip;
    public BooleanAction OculusRightGrip;
    public BooleanAction SimulatorLeftGrip;
    public BooleanAction SimulatorRightGrip;

    public GameObject instrumentDisplays;

    // Start is called before the first frame update
    void Start()
    {
        curXRSetup = xrSetup;
    }

    // Update is called once per frame
    void OnValidate()
    {
        Debug.Log("OnValidate");
        if (curXRSetup != xrSetup)
        {
            //StartCoroutine (ChangeXRSettings());
            ChangeXRSettings();
            curXRSetup = xrSetup;
        }
    }

    void ChangeXRSettings()
    {
        bool switchToOculus = xrSetup == XRsetup.UseOculus;
        Debug.Log(xrSetup == XRsetup.UseOculus ? "Changing to Oculus" : "Changing to Simulator");

        Debug.Log("Activating correct GameObject");
        // Activate the correct GameObject
        OculusCameraRig.gameObject.SetActive (switchToOculus);
        SimulatorCameraRig.gameObject.SetActive (!switchToOculus);

        Debug.Log("Change interactor grip settings");

        // Change interactor grip settings
        var leftSO = new SerializedObject(LeftInteractorObj.GetComponent<InteractorFacade>());
        var rightSO = new SerializedObject(RightInteractorObj.GetComponent<InteractorFacade>());

        leftSO.FindProperty("grabAction").objectReferenceValue = switchToOculus ? OculusLeftGrip : SimulatorLeftGrip;
        leftSO.ApplyModifiedProperties();

        rightSO.FindProperty("grabAction").objectReferenceValue = switchToOculus ? OculusRightGrip : SimulatorRightGrip;
        rightSO.ApplyModifiedProperties();

        // LeftInteractorObj.GetComponent<InteractorFacade>().GrabAction = switchToOculus ? OculusLeftGrip : SimulatorLeftGrip;
        // RightInteractorObj.GetComponent<InteractorFacade>().GrabAction = switchToOculus ? OculusRightGrip : SimulatorRightGrip;


        Debug.Log("Change the Tracked alias list");
        // Change the Tracked alias list
        // var trackedAliasSO = new SerializedObject(TrackedAliasObj.GetComponent<TrackedAliasFacade>().CameraRigs);

        // LinkedAliasAssociationCollectionObservableList listTest;
        // listTest.Add(switchToOculus ? OculusCameraRig : SimulatorCameraRig);
        // trackedAliasSO.FindProperty("elements.Array.data[0]").objectReferenceValue = switchToOculus ? OculusCameraRig : SimulatorCameraRig;

        // trackedAliasSO.FindProperty("elements").objectReferenceValue = listTest;
        TrackedAliasObj.GetComponent<TrackedAliasFacade>().CameraRigs.Clear();
        TrackedAliasObj.GetComponent<TrackedAliasFacade>().CameraRigs.Add(switchToOculus ? OculusCameraRig : SimulatorCameraRig);

        
        foreach (Transform child in instrumentDisplays.transform)
        {
            if (child.GetChild(0).tag == "Instrument")
            {                
                var toggleVar = Tilia.Interactions.Interactables.Interactables.Grab.Receiver.GrabInteractableReceiver.ActiveType.Toggle;
                var holdTillReleaseVar = Tilia.Interactions.Interactables.Interactables.Grab.Receiver.GrabInteractableReceiver.ActiveType.HoldTillRelease;

                var interactableSO = new SerializedObject(child.GetChild(0).GetComponent<InteractableFacade>());
                interactableSO.FindProperty("grabType").enumValueIndex = switchToOculus ? 0 : 1;
                Debug.Log(interactableSO.FindProperty("grabType").enumValueIndex);
                interactableSO.ApplyModifiedProperties();

            }
        }


    }
}
