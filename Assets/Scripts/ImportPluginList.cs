using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;



[ExecuteInEditMode]
public class ImportPluginList : MonoBehaviour
{
    public GameObject instrumentDisplays;
    // Start is called before the first frame update

    public List<String> pluginNames;
    void Start()
    {
        for (int i = 0; i < getNumPresets(); ++i)
        {
            String pluginName = Marshal.PtrToStringAuto (getPresetAt(i));
            pluginName = pluginName.Split('_')[0];
            if (!pluginNames.Contains (pluginName))
            {
                Debug.Log (pluginName);
                pluginNames.Add (pluginName);
            }

        }
        foreach (Transform child in instrumentDisplays.transform)
        {
            if (child.GetChild(0).tag == "Instrument")
            {
                GameObject model = child.GetChild(0).GetChild(0).GetChild(1).gameObject;
                if (model.tag != "Model")
                {
                    Debug.LogWarning("Should be looking at model here!");
                    continue;
                }
                Debug.Log("Adding to " + model.name + "'s list");
                for (int i = 0; i < getNumPresets(); ++i)
                    if (!model.GetComponent<SelectPreset>().pluginList.Contains(pluginNames[i]))
                        model.GetComponent<SelectPreset>().pluginList.Add(pluginNames[i]);
                Debug.Log("Adding to NameList");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    [DllImport("audioPlugin_ModularVST", CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr getPresetAt(int i);

    [DllImport("audioPlugin_ModularVST", CallingConvention = CallingConvention.Cdecl)]
    static extern int getNumPresets();
}
