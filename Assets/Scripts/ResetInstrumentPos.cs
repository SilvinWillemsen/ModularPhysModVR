using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetInstrumentPos : MonoBehaviour
{
    InstrumentReferenceList instrumentReferenceList;

    [SerializeField] private float timeBeforeDespawn = 1.0f;
    [SerializeField] private float despawnTime = 1.0f;
    [SerializeField] private float spawnTime = 1.0f;
    [SerializeField] private float transitionTime = 0.5f;
    private void Start()
    {
        instrumentReferenceList = GetComponent<InstrumentReferenceList>();
    }
    public void DespawnAndSpawnInstrument(GameObject instrument)
    {
        // List<GameObject> thisInstrument = new List<GameObject>();
        // thisInstrument.Add(instrument);
        StartCoroutine(StartResetCoroutine(instrument, despawnTime, spawnTime, transitionTime)); 
    }

    IEnumerator StartResetCoroutine(GameObject thisInstrument, float despawnTime, float spawnTime, float transitionTime)
    {
        yield return new WaitForSeconds(timeBeforeDespawn);
        Global.DespawnSingleInteractable(thisInstrument.transform.GetChild(0), despawnTime, false);
        yield return new WaitForSeconds(transitionTime + despawnTime); // wait for despawnTime + transition time before spawning agia
        thisInstrument.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.localScale = new Vector3(1e-5f, 1e-5f, 1e-5f);

        // Find index of instrument to spawn
        int idx = -1;
        int i = 0;
        foreach (GameObject instrument in instrumentReferenceList.instruments)
        {
            if (thisInstrument == instrument)
                idx = i;
            ++i;
        }

        if (idx == -1)
        {
            Debug.LogError("instrumentNotFound!");
        } 
        else 
        {
            Debug.Log("Index of model to spawn is " + idx);
            Global.SpawnSingleInteractable(thisInstrument.transform.GetChild(0), spawnTime, instrumentReferenceList.instrumentStartPos[idx], instrumentReferenceList.instrumentStartOrientation[idx]);
        }
    }
}
