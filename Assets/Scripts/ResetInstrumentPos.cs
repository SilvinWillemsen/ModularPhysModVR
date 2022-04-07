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
        List<GameObject> thisInstrument = new List<GameObject>();
        thisInstrument.Add(instrument);
        StartCoroutine(StartResetCoroutine(thisInstrument, despawnTime, spawnTime, transitionTime)); 
    }

    IEnumerator StartResetCoroutine(List<GameObject> thisInstrument, float despawnTime, float spawnTime, float transitionTime)
    {
        yield return new WaitForSeconds(timeBeforeDespawn);
        Global.DespawnInstruments(thisInstrument, despawnTime, false);
        yield return new WaitForSeconds(transitionTime + despawnTime); // wait for despawnTime + transition time before spawning agia
        thisInstrument[0].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.localScale = Vector3.zero;
        Global.SpawnInstruments(thisInstrument, spawnTime, instrumentReferenceList.instrumentStartPos, instrumentReferenceList.instrumentStartOrientation);
    }
}
