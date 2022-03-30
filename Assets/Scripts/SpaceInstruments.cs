using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInstruments : MonoBehaviour
{
   public void SpaceEqually(GameObject[] instruments, float radius)
    {
        for (int i = 0; i < instruments.Length; i++)
        {
            float angle = i * Mathf.PI * 2f / instruments.Length;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, 0.0f, Mathf.Sin(angle) * radius);
            instruments[i].transform.localPosition = newPos;
        }
    }
}
