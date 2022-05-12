using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetInstrumentPos : MonoBehaviour
{
    InstrumentReferenceList instrumentReferenceList;
    [SerializeField] GameObject instrumentStage;

    Vector3 instrumentStageLoc; 
    [SerializeField] private float timeBeforeDespawn = 1.0f;
    [SerializeField] private float despawnTime = 1.0f;
    [SerializeField] private float spawnTime = 1.0f;
    [SerializeField] private float transitionTime = 0.5f;
    private void Start()
    {
        instrumentReferenceList = GetComponent<InstrumentReferenceList>();
        instrumentStageLoc = instrumentStage.transform.position;
        instrumentStageLoc.y += 0.5f;
    }
    public void DespawnAndSpawnInstrument(GameObject instrument)
    {
        // List<GameObject> thisInstrument = new List<GameObject>();
        // thisInstrument.Add(instrument);

        bool moveToStage = false;
        // check if need to be moved to stage
        foreach(Transform child in instrument.transform)
        {
            if(child.tag=="Instrument")
            {
                if (child.transform.GetChild(0).transform.GetChild(1).GetComponent<CustomGrabAttachment>() != null)
                {
                    moveToStage = child.transform.GetChild(0).transform.GetChild(1).GetComponent<CustomGrabAttachment>().moveToStageWhenGrabbed;
                }
            }    
        }
        
        StartCoroutine(StartResetCoroutine(instrument, despawnTime, spawnTime, transitionTime, moveToStage)); 
    }

    IEnumerator StartResetCoroutine(GameObject thisInstrument, float despawnTime, float spawnTime, float transitionTime, bool moveToStage)
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
            if(moveToStage)
            {
                Global.SpawnSingleInteractable(thisInstrument.transform.GetChild(0), spawnTime, instrumentStageLoc , instrumentReferenceList.instrumentStartOrientation[idx], true);

            }
            else
            {
                Global.SpawnSingleInteractable(thisInstrument.transform.GetChild(0), spawnTime, instrumentReferenceList.instrumentStartPos[idx], instrumentReferenceList.instrumentStartOrientation[idx], false);

            }
        }
    }
}
