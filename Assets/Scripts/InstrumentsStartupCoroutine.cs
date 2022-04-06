using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentsStartupCoroutine : MonoBehaviour
{

    private GameObject[] instruments;
    private List<Vector3> instrumentStartScales = new List<Vector3>(); 
    [SerializeField] private float radiusToOrigin = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        instruments = GetComponent<InstrumentReferenceList>().instruments;

        foreach (GameObject instrument in instruments)
        {
            Debug.Log("Looking at " + instrument.name);
            foreach (Transform child in instrument.transform)
            {
                if (child.tag == "Instrument")
                {
                    // Debug.Log("Looking at " + child.GetChild(0).name);
                    // child.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    child.GetChild(0).gameObject.AddComponent<ResetGravity>();
                    instrumentStartScales.Add(child.transform.localScale);
                }
            }
        }
        StartCoroutine(Startup());
    }
    IEnumerator Startup()
    {
        // disable all instruments, prevent Unity crashing
        Global.DespawnInstruments(instruments);

        // Space all instruments around the origin 
        Global.SpaceEqually(instruments, radiusToOrigin);

        // Make all instruments face origin
        Global.FaceInstrumentsToOrigin(instruments);

        yield return new WaitForSeconds(1.0f);

        // Respawn instruments;
        Global.SpawnInstruments(instruments, instrumentStartScales);
    }
}
