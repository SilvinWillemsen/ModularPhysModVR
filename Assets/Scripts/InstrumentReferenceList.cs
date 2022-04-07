using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentReferenceList : MonoBehaviour
{

    [HideInInspector] public List<GameObject> instruments = new List<GameObject>();
    [HideInInspector] public List<Vector3> instrumentStartPos = new List<Vector3>();
    private void Awake()
    {
        foreach(Transform child in transform)
        {
            instruments.Add(child.gameObject);
        }
    }
}
