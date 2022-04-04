using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SelectPreset : MonoBehaviour
{

    private float selectedPreset = 0.0f;

    public AudioMixer audioMixer;

    public enum InstrumentType
    {
        Guitar,
        Harp,
        TwoStrings,
        BanjoLele
    }

    [SerializeField] InstrumentType instrumentType;

    // Start is called before the first frame update
    void Start()
    {
        int nPresets = 4;


        // changing instrument preset
        int chosenInstrument = 0;
        switch (instrumentType)
        {
            case InstrumentType.Guitar:
                chosenInstrument = (int)InstrumentType.Guitar;
                break;

            case InstrumentType.Harp:
                chosenInstrument = (int)InstrumentType.Harp;
                break;

            case InstrumentType.TwoStrings:
                chosenInstrument = (int)InstrumentType.TwoStrings;
                break;

            case InstrumentType.BanjoLele:
                chosenInstrument = (int)InstrumentType.BanjoLele;
                break;
            default:
                Debug.Log("Please specify instrument type from inspector");
                break;
        }


        selectedPreset = (float)chosenInstrument / nPresets;
        Debug.Log (chosenInstrument);
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


    //[DllImport("audioPlugin_ModularVST", CallingConvention = CallingConvention.Cdecl)]
    //static extern IntPtr getPresetAt(int i);

    //[DllImport("audioPlugin_ModularVST", CallingConvention = CallingConvention.Cdecl)]
    //static extern int getNumPresets();
}
