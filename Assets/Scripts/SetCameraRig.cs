using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tilia.CameraRigs.TrackedAlias;
using Zinnia.Tracking.CameraRig;
using Tilia.Interactions.Interactables.Interactors;
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
    public GameObject LeftInteractorObj;
    public GameObject RightInteractorObj;

    public BooleanAction OculusLeftGrip;
    public BooleanAction OculusRightGrip;
    public BooleanAction SimulatorLeftGrip;
    public BooleanAction SimulatorRightGrip;

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
        LeftInteractorObj.GetComponent<InteractorFacade>().GrabAction = switchToOculus ? OculusLeftGrip : SimulatorLeftGrip;
        RightInteractorObj.GetComponent<InteractorFacade>().GrabAction = switchToOculus ? OculusRightGrip : SimulatorRightGrip;


        Debug.Log("Change the Tracked alias list");
        // Change the Tracked alias list
        TrackedAliasObj.GetComponent<TrackedAliasFacade>().CameraRigs.Clear();
        TrackedAliasObj.GetComponent<TrackedAliasFacade>().CameraRigs.Add(switchToOculus ? OculusCameraRig : SimulatorCameraRig);

    }
}
