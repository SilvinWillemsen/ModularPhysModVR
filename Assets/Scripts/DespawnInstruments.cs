using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnInstruments : MonoBehaviour
{
    private GameObject[] instruments;

    public void Despawn(GameObject[] instruments)
    {
        foreach (GameObject instrument in instruments)
        {
            foreach (Transform child in instrument.transform)
            {
                if (child.tag == "Instrument")
                {
                    child.gameObject.SetActive(false);
                }
            }

        }
    }
}
