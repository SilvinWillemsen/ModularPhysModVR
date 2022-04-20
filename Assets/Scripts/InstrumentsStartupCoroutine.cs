using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentsStartupCoroutine : MonoBehaviour
{
    [SerializeField] private float radiusToOrigin = 5.0f;
    [SerializeField] private int maxNInstruments = 10;

    private List<GameObject> instruments;
    private List<Vector3> instrumentStartPos = new List<Vector3>();
    private List<Quaternion> instrumentStartOrientation = new List<Quaternion>();

    private InstrumentReferenceList instrumentReferenceList; 

    // Start is called before the first frame update
    void Start()
    {
        instrumentReferenceList = GetComponent<InstrumentReferenceList>();

        instruments = instrumentReferenceList.instruments;
       
        foreach (GameObject instrument in instruments)
        {
            foreach (Transform child in instrument.transform)
            {
                if (child.tag == "Instrument")
                {
                    //child.gameObject.AddComponent<IsGrabbed>(); // add grab script for custom grab interaction
                    // Debug.Log("Looking at " + child.GetChild(0).name);
                    // child.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    child.GetChild(0).gameObject.AddComponent<AnimationCallBack>();

                    instrumentStartPos.Add(child.gameObject.transform.localPosition);
                    instrumentStartOrientation.Add(child.gameObject.transform.localRotation);
                }
            }
        }

        instrumentReferenceList.instrumentStartPos = instrumentStartPos;
        instrumentReferenceList.instrumentStartOrientation = instrumentStartOrientation;
        StartCoroutine(Startup());
    }
    IEnumerator Startup()
    {
        // disable all instruments, prevent Unity crashing
        Global.DespawnInstruments(instruments, 0.1f, true);

        // Space all instruments around the origin 
        Global.SpaceEqually(instruments, radiusToOrigin, maxNInstruments);

        // Make all instruments face origin
        Global.FaceInstrumentsToOrigin(instruments);

        yield return new WaitForSeconds(1.0f);

        // Respawn instruments;
        Global.SpawnInstruments(instruments, 1.0f , instrumentStartPos, instrumentStartOrientation);
    }
}
