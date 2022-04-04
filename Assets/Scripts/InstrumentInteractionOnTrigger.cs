using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class InstrumentInteractionOnTrigger : MonoBehaviour
{
    public enum StringOrientation
    {
        Horizontal,
        Vertical,
    };

    [SerializeField] AudioMixer audioMixer;    

    private float excitationType;
    private GameObject excitationLoc;
    [SerializeField] private StringOrientation stringOrientation;

    //public float selectedPreset = 0.0f;


    // Start is called before the first frame update
    void Start()
    {        
        int nPresets = System.Enum.GetValues(typeof(InstrumentType)).Length;
        // changing instrument preset
        int chosenInstrument = 0;
        //switch (instrumentType)
        //{
        //    case InstrumentType.Guitar:
        //        chosenInstrument = (int)InstrumentType.Guitar;
        //        break;

        //    case InstrumentType.Harp:
        //        chosenInstrument = (int)InstrumentType.Harp;
        //        break;

        //    case InstrumentType.TwoStrings:
        //        chosenInstrument = (int)InstrumentType.TwoStrings;
        //        break;

        //    case InstrumentType.banjoline:
        //        chosenInstrument = (int)InstrumentType.banjoline;
        //        break;
        //    default:
        //        Debug.Log("Please specify instrument type from inspector");
        //        break;
        //}

        /*[DllImport("audioPlugin_ModularVST", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr getPresetAt(int i);

        [DllImport("audioPlugin_ModularVST", CallingConvention = CallingConvention.Cdecl)]
        static extern int getNumPresets();*/

        //selectedPreset = (float)chosenInstrument / nPresets;
        //Debug.Log(chosenInstrument);
        //StartCoroutine(ChangePreset());
        // create an object indicating where it is excited 
        excitationLoc = Instantiate(new GameObject(), transform);
        excitationLoc.name = "excitationLoc";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ExciterArea")
        {
            //Debug.Log("Excited");

            // calculate where on the instrument it's exciting:
            excitationLoc.transform.position = other.transform.position;

            Vector3 localPos = new Vector3(excitationLoc.transform.localPosition.x, excitationLoc.transform.localPosition.y, excitationLoc.transform.localPosition.z);

            float xBounds = transform.localScale.x;
            float yBounds = transform.localScale.y;

            //Debug.Log("x pos = " + localPos.x + "y pos = " + localPos.y);
            //Debug.Log("xBbounds = " + xBounds + ", yBounds = " + yBounds);

            // map & limit values, swap value for juce
            float xPos = 1.0f - Global.Limit(Global.Map(localPos.x, -xBounds / 2, xBounds / 2, 0, 1), 0, 1);
            float yPos = 1.0f - Global.Limit(Global.Map(localPos.y, -yBounds / 2, yBounds / 2, 0, 1), 0, 1);


            // map according to the string orientation
            
            // Flip x and y positions if vertical
            audioMixer.SetFloat("mouseX", stringOrientation == StringOrientation.Vertical ? yPos : xPos);
            audioMixer.SetFloat("mouseY", stringOrientation == StringOrientation.Vertical ? xPos : yPos);

            // visual representation
            //excitelocIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos * 10, yPos * 10);

        }
    }

    private void OnTriggerExit(Collider other)
    {
    }

    IEnumerator ChangePreset()
    {
        //audioMixer.SetFloat("presetSelect", selectedPreset);
        yield return new WaitForSeconds(0.1f);
        audioMixer.SetFloat("loadPreset", 1.0f);
        yield return new WaitForSeconds(0.1f);
        audioMixer.SetFloat("loadPreset", 0.0f);
    }



}
