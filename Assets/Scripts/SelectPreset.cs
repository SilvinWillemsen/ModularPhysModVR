using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;
using UnityEngine.Audio;


public class SelectPreset : MonoBehaviour
{

    private float selectedPreset = 0.0f;

    public AudioMixer audioMixer;

    // public List<string> NameList; 
     
    // [Dropdown("NameList")]//input the path of the list
    //  public string MyName;
    [SerializeField] Global.InstrumentType instrumentType;

        // Start is called before the first frame update
    void Start()
    {
        int nPresets = getNumPresets();

        // changing instrument preset
        if (gameObject.GetComponentInChildren<PlayAreaInteraction>() == null)
            Debug.LogWarning("There is no playarea in this instrument!");
        else
            gameObject.GetComponentInChildren<PlayAreaInteraction>().instrumentType = instrumentType;
        // switch (instrumentType)
        // {
        //     case InstrumentType.Guitar:
        //         chosenInstrument = (int)InstrumentType.Guitar;
        //         break;

        //     case InstrumentType.Harp:
        //         //transform.GetChild(0).GetComponent<PlayAreaInteraction>().instrumentType = "Harp";
        //         chosenInstrument = (int)InstrumentType.Harp;
        //         break;

        //     case InstrumentType.TwoStrings:
        //         chosenInstrument = (int)InstrumentType.TwoStrings;
        //         break;

        //     case InstrumentType.BanjoLele:
        //         chosenInstrument = (int)InstrumentType.BanjoLele;
        //         break;
        //     default:
        //         Debug.Log("Please specify instrument type from inspector");
        //         break;
        // }


        selectedPreset = (float)instrumentType / nPresets;
        Debug.Log ((int)instrumentType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstrumentGrabbed()
    {
        StartCoroutine(ChangePreset());
    }


    IEnumerator ChangePreset()
    {
        audioMixer.SetFloat("presetSelect", selectedPreset);
        yield return new WaitForSeconds(0.1f);
        audioMixer.SetFloat("loadPreset", 1.0f);
        yield return new WaitForSeconds(0.1f);
        audioMixer.SetFloat("loadPreset", 0.0f);
    }

    [DllImport("audioPlugin_ModularVST", CallingConvention = CallingConvention.Cdecl)]
    static extern int getNumPresets();


}
