using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetExciterPos : MonoBehaviour
{
    // Start is called before the first frame update

    ExciterReferenceList exciterReferenceList;

    [SerializeField] private float timeBeforeDespawn = 1.0f;
    [SerializeField] private float despawnTime = 1.0f;
    [SerializeField] private float spawnTime = 1.0f;
    [SerializeField] private float transitionTime = 0.5f;

    void Start()
    {
        exciterReferenceList = GetComponent<ExciterReferenceList>();
    }
    public void DespawnAndSpawnExciter(GameObject exciter)
    {
        List<GameObject> thisInstrument = new List<GameObject>();
        thisInstrument.Add(exciter);
        StartCoroutine(StartResetCoroutine(thisInstrument, despawnTime, spawnTime, transitionTime));
    }

    IEnumerator StartResetCoroutine(List<GameObject> thisInstrument, float despawnTime, float spawnTime, float transitionTime)
    {
        yield return new WaitForSeconds(timeBeforeDespawn);
        Global.DespawnInstruments(thisInstrument, despawnTime, false);
        yield return new WaitForSeconds(transitionTime + despawnTime); // wait for despawnTime + transition time before spawning agia
        thisInstrument[0].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.localScale = new Vector3 (1e-5f, 1e-5f, 1e-5f);
        Global.SpawnInstruments(thisInstrument, spawnTime, exciterReferenceList.exciterStartPos, exciterReferenceList.exciterStartOrientation);
    }
}
