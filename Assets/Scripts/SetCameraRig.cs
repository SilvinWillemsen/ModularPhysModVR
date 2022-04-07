using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tilia.CameraRigs.TrackedAlias;
using Zinnia.Tracking.CameraRig;

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

    public GameObject TrackedAlias;
    public LinkedAliasAssociationCollection OculusCameraRig;
    public LinkedAliasAssociationCollection SimulatorCameraRig;

    // Start is called before the first frame update
    void Start()
    {
        curXRSetup = xrSetup;
    }

    // Update is called once per frame
    void Update()
    {
        if (curXRSetup != xrSetup)
        {
            ChangeXRSettings();
            curXRSetup = xrSetup;
        }
    }

    void ChangeXRSettings()
    {
        bool switchToOculus = xrSetup == XRsetup.UseOculus;
        Debug.Log(xrSetup == XRsetup.UseOculus ? "Changing to Oculus" : "Changing to Simulator");
        OculusCameraRig.gameObject.SetActive (switchToOculus);
        SimulatorCameraRig.gameObject.SetActive (!switchToOculus);

        TrackedAlias.GetComponent<TrackedAliasFacade>().CameraRigs.Clear();

        TrackedAlias.GetComponent<TrackedAliasFacade>().CameraRigs.Add(switchToOculus ? OculusCameraRig : SimulatorCameraRig);
    }
}