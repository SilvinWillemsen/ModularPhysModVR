using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    //public static Global instance;

    /*private static void Init()
    {
        //If the instance not exit the first time we call the static class
        if (instance == null)
        {
            //Create an empty object called MyStatic
            GameObject gameObject = new GameObject("MyStatic");


            //Add this script to the object
            instance = gameObject.AddComponent<Global>();
        }
    }*/

    public static void DespawnInstruments(List<GameObject> instruments,float despawnTime, bool disableGravity)
    {
        //Init();
        foreach (GameObject instrument in instruments)
        {
            foreach (Transform child in instrument.transform)
            {
                if (child.tag == "Instrument")
                {
                    if (disableGravity) child.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    GameObject target = child.GetChild(0).gameObject;
                    // iTween.ScaleTo(target, Vector3.zero, 0.5f);
                    iTween.ScaleTo(target, new Vector3(1e-5f, 1e-5f, 1e-5f), despawnTime);
                }
            }
        }
    }

    public static void SpawnInstruments(List<GameObject> instruments, float spawnTime , List<Vector3> instrumentStartPos)
    {
        //Init();
        int i = 0; 
        foreach (GameObject instrument in instruments)
        {
            foreach (Transform child in instrument.transform)
            {
                if (child.tag == "Instrument")
                {
                    child.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    GameObject target = child.GetChild(0).gameObject;
                    target.transform.localPosition = instrumentStartPos[i];
                    iTween.ScaleTo(target, iTween.Hash("x", 1.0f, "y", 1.0f, "z", 1.0f, "time", spawnTime, "onComplete", "TurnGravityOn"));
                    child.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    i++;
                }
            }
        }
    }

    public static void SpaceEqually(List<GameObject> instruments, float radius, int maxNInstruments)
    {
        if (instruments.Count > maxNInstruments) Debug.Log("Inserted instruments exceeding max number of instruments specified!");
        float angleOffset = - Mathf.PI / (instruments.Count + 1);
        for (int i = 0; i < maxNInstruments; i++)
        {
            float angle = i * Mathf.PI * 2f / maxNInstruments - angleOffset;
            if (i < instruments.Count)
            {
                Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, 0.0f, Mathf.Sin(angle) * radius);
                instruments[i].transform.localPosition = newPos;
            }
        }
    }

    public static void FaceInstrumentsToOrigin(List<GameObject> instruments)
    {
        for (int i = 0; i < instruments.Count; i++)
        {
            instruments[i].transform.LookAt(Vector3.zero, Vector3.up);
        }
    }

    public static float Map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static float Limit(float val, float min, float max)
    {
        if (val < min) return min;
        else if (val > max) return max;
        else return val;
    }



}
