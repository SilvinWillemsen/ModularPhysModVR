using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;


public enum InstrumentType
{
    Guitar,
    Harp,
    TwoStrings,
    banjoline
};

public enum StringOrientation
{
    Horizontal,
    Vertical,
};

public class ExciteInstrumentOnTrigger : MonoBehaviour
{
    [SerializeField] private InstrumentType instrumentType;
    [SerializeField] private StringOrientation stringOrientation;
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Image excitelocIcon;

    private float excitationType;

    private GameObject excitationLoc;

    public float selectedPreset = 0.0f; 

    // Start is called before the first frame update
    void Start()
    {
        int nPresets = System.Enum.GetValues(typeof(InstrumentType)).Length;
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

            case InstrumentType.banjoline:
                chosenInstrument = (int)InstrumentType.banjoline;
                break;
            default:
                Debug.Log("Please specify instrument type from inspector");
                break;
        }

        /*[DllImport("audioPlugin_ModularVST", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr getPresetAt(int i);

        [DllImport("audioPlugin_ModularVST", CallingConvention = CallingConvention.Cdecl)]
        static extern int getNumPresets();*/

        selectedPreset  = (float)chosenInstrument / nPresets;
        Debug.Log(chosenInstrument);
        StartCoroutine(ChangePreset());
        // create an object indicating where it is excited 
        excitationLoc = Instantiate(new GameObject(), transform);
        excitationLoc.name = "excitationLoc";

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(instrumentCollider.bounds.size.y);

    }

    private void OnTriggerEnter(Collider other)
    {
        // change preset (in case of more instruments in one scene)
        //StartCoroutine(ChangePreset());

        audioMixer.SetFloat("excite", 1.0f);

        if (other.gameObject.tag == "Pick")
        {
            excitationType = 0.0f;
        }
        else if (other.gameObject.tag == "Hammer")
        {
            excitationType = 0.5f;
        }
        else if (other.gameObject.tag == "Bow")
        {
            excitationType = 0.8f;
        }
        else
        {
            // ignore collision
        }

        // apply
        audioMixer.SetFloat("excitationType", excitationType);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Pick" || other.gameObject.tag == "Hammer" || other.gameObject.tag == "Bow")
        {
            Debug.Log("Excited");

            // calculate where on the instrument it's exciting:
            excitationLoc.transform.position = other.transform.position;

            Vector3 localPos = new Vector3(excitationLoc.transform.localPosition.x, excitationLoc.transform.localPosition.y, excitationLoc.transform.localPosition.z);

            float xBounds = transform.localScale.x;
            float yBounds = transform.localScale.y;

            //Debug.Log("x pos = " + localPos.x + "y pos = " + localPos.y);
            //Debug.Log("xBbounds = " + xBounds + ", yBounds = " + yBounds);
            
            // map & limit values, swap value for juce
            float xPos = 1.0f - Limit(Map(localPos.x, -xBounds / 2, xBounds / 2, 0, 1), 0, 1);
            float yPos = 1.0f - Limit(Map(localPos.y, -yBounds / 2, yBounds / 2, 0, 1), 0, 1);

           
            // map according to the string orientation
            switch (stringOrientation)
            {
                case StringOrientation.Horizontal:
                    audioMixer.SetFloat("mouseX", xPos);
                    audioMixer.SetFloat("mouseY", yPos);
                    break;

                case StringOrientation.Vertical:
                    audioMixer.SetFloat("mouseX", yPos);
                    audioMixer.SetFloat("mouseY", xPos);
                    break;
            }

            // visual representation
            excitelocIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos * 10, yPos * 10);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        audioMixer.SetFloat("excite", 0.0f);
    }

    float Map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    float Limit(float val, float min, float max)
    {
        if (val < min) return min;
        else if (val > max) return max;
        else return val;
    }
    
    IEnumerator ChangePreset()
    {
        audioMixer.SetFloat("presetSelect", selectedPreset);
        yield return new WaitForSeconds(0.1f);
        audioMixer.SetFloat("loadPreset", 1.0f);
        yield return new WaitForSeconds(0.1f);
        audioMixer.SetFloat("loadPreset", 0.0f);
    }


}
