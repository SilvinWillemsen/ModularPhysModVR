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

    public static void DespawnInstruments(GameObject[] instruments)
    {
        //Init();
        foreach (GameObject instrument in instruments)
        {
            foreach (Transform child in instrument.transform)
            {
                if (child.tag == "Instrument")
                {
                    child.gameObject.SetActive(false);

                    //instance.StartCoroutine(Fade(child.gameObject, 0.0f, 1.0f));
                }
            }
        }
    }

    public static void SpawnInstruments(GameObject[] instruments)
    {
        //Init();
        foreach (GameObject instrument in instruments)
        {
            foreach (Transform child in instrument.transform)
            {
                if (child.tag == "Instrument")
                {
                    child.gameObject.SetActive(true);
                    
                    //instance.StartCoroutine(Fade(child.gameObject, 1.0f, 1.0f));

                    
                }
            }
        }
    }

    public static IEnumerator Fade(GameObject gameObject, float targetVal, float fadeTime)
    {
        float currentTime = 0.0f;
        Material mat = gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material;
        float startVal = mat.color.a; 
        Color color = mat.color; 

        if(currentTime < fadeTime)
        {
            color.a = Mathf.Lerp(startVal, targetVal, currentTime / fadeTime);
            mat.color = color; 
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    /*public static void SetMaterialsTransparent(GameObject gameObject)
    {
        foreach(Material mat in gameObject.GetComponent<Renderer>().materials)
        {
            mat.SetFloat("_Mode", 2);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000; 
        }
    }*/

  /*  public static void SetMaterialsOpaque(GameObject gameObject)
    {
        foreach (Material mat in gameObject.GetComponent<Renderer>().materials)
        {
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            mat.SetInt("_ZWrite", 1);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.DisableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = -1;
        }
    }*/

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
