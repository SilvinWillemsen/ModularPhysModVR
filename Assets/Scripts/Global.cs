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

    public enum InstrumentType
    {
        Guitar,
        Harp,
        TwoStrings,
        BanjoLele
    }

    public static void DespawnInstruments(GameObject[] instruments)
    {
        //Init();
        foreach (GameObject instrument in instruments)
        {
            foreach (Transform child in instrument.transform)
            {
                if (child.tag == "Instrument")
                {
                    child.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    GameObject target = child.GetChild(0).gameObject;
                    // iTween.ScaleTo(target, Vector3.zero, 0.5f);
                    iTween.ScaleTo(target, new Vector3(1e-5f, 1e-5f, 1e-5f), 0.5f);
                }
            }
        }
    }

    public static void SpawnInstruments(GameObject[] instruments, List<Vector3> instrumentScales)
    {
        //Init();
        foreach (GameObject instrument in instruments)
        {
            foreach (Transform child in instrument.transform)
            {
                if (child.tag == "Instrument")
                {
                    GameObject target = child.GetChild(0).gameObject;
                    iTween.ScaleTo(target, iTween.Hash("x", 1.0f, "y", 1.0f, "z", 1.0f, "time", 0.5f, "onComplete", "TurnGravityOn"));
                    // child.gameObject.GetComponent<Rigidbody>().useGravity = true;
                }
            }
        }
    }

    public static void SpaceEqually(GameObject[] instruments, float radius)
    {
        for (int i = 0; i < instruments.Length; i++)
        {
            float angle = i * Mathf.PI * 2f / instruments.Length;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, 0.0f, Mathf.Sin(angle) * radius);
            instruments[i].transform.localPosition = newPos;
        }
    }

    public static void FaceInstrumentsToOrigin(GameObject[] instruments)
    {
        for (int i = 0; i < instruments.Length; i++)
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
