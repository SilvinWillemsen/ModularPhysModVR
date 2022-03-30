using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceInstruments : MonoBehaviour
{ 
    public void FaceInstrumentsToOrigin(GameObject[] instruments)
    {
        
        for(int i =0; i < instruments.Length; i ++)
        {
            instruments[i].transform.LookAt(Vector3.zero, Vector3.up);
        }
    }
}
