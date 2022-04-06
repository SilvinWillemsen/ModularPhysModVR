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
    void Start()
    {
        for (int i = 0; i < getNumPresets(); ++i)
        {
            Debug.Log (Marshal.PtrToStringAuto (getPresetAt(i)));
        }
        foreach (Transform child in instrumentDisplays.transform)
        {
            if (child.tag == "Model")
            {
                // for (int i = 0; i < getNumPresets(); ++i)
                    // child.GetComponent<SelectPreset>().NameList.Add(names[i]);
                
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
