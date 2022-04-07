using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetInstrumentPos : MonoBehaviour
{
    InstrumentReferenceList instrumentReferenceList;

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
        Global.DespawnInstruments(thisInstrument, despawnTime, false);
        thisInstrument[0].transform.GetChild(0).gameObject.SetActive(false); 
        yield return new WaitForSeconds(transitionTime);
        thisInstrument[0].transform.GetChild(0).gameObject.SetActive(true);
        Global.SpawnInstruments(thisInstrument, spawnTime, instrumentReferenceList.instrumentStartPos);
    }
}
