using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentsStartupCoroutine : MonoBehaviour
{

    private GameObject[] instruments;
    [SerializeField] private float radiusToOrigin = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        instruments = GetComponent<InstrumentReferenceList>().instruments;
        StartCoroutine(Startup());
    }
    IEnumerator Startup()
    {
        // disable all instruments, prevent Unity crashing
        GetComponent<DespawnInstruments>().Despawn(instruments);

        // Space all instruments around the origin 
        GetComponent<SpaceInstruments>().SpaceEqually(instruments, radiusToOrigin);

        // Make all instruments face origin
        GetComponent<FaceInstruments>().FaceInstrumentsToOrigin(instruments);

        yield return new WaitForSeconds(0.5f);

        // Respawn instruments;
        GetComponent<SpawnInstruments>().Spawn(instruments);
    }
}
