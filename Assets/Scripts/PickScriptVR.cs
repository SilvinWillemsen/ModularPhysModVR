using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PickScriptVR : MonoBehaviour
{
    public AudioMixer audioMixer;
    private int curExcitationType;
    private float smoothness;
    private bool scrollForExcitation = true;


    // Start is called before the first frame update
    Collider m_Collider;
    float m_Min_x, m_Max_x, m_Min_y, m_Max_y, x_range, y_range;
    void Start()
    {
        float outFloat;
        if (scrollForExcitation)
        {
            audioMixer.GetFloat("excitationType", out outFloat);
            if (outFloat < 0.33f)
            {
                curExcitationType = 0;
            }
            else if (outFloat < 0.67f)
            {
                curExcitationType = 1;
            }
            else
            {
                curExcitationType = 2;
            }

        }
        else
        {
            audioMixer.GetFloat("smoothness", out outFloat);
            smoothness = outFloat;
        }

        OutputData();
    }

    public void setColliderInfo (GameObject instrument)
    {
        foreach(Transform child in instrument.transform)
            if (child.tag == "PlayArea")
                m_Collider = child.GetComponent<Collider>();
        //Fetch the size of the Collider volume
        //Fetch the minimum and maximum bounds of the Collider volume
        m_Min_x = m_Collider.bounds.min.x;
        m_Max_x = m_Collider.bounds.max.x;
        m_Min_y = m_Collider.bounds.min.y;
        m_Max_y = m_Collider.bounds.max.y;

        x_range = m_Max_x - m_Min_x;
        y_range = m_Max_y - m_Min_y;

        OutputData();

    }

    void OutputData()
    {
        //Output to the console the center and size of the Collider volume
        Debug.Log("Collider bound Minimum x: " + m_Min_x);
        Debug.Log("Collider bound Maximum x: " + m_Max_x);
        Debug.Log("Collider bound Minimum y: " + m_Min_y);
        Debug.Log("Collider bound Maximum y: " + m_Max_y);
    }
    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator triggerHammer()
    {
        audioMixer.SetFloat("trigger", 1.0f);
        yield return new WaitForSeconds(0.1f);

        audioMixer.SetFloat("trigger", 0.0f);
    }



    void OnTriggerStay (Collider other)
    {
        if (other.tag != "PlayArea")
            return;
        Debug.Log(other.tag);

        string currentPresetName = "guitar_xml";

        float ratioLocX = (other.gameObject.transform.localPosition.x - m_Min_x) / x_range;
        float ratioLocY = (1.0f - (other.gameObject.transform.localPosition.y - m_Min_y) / y_range);
        if (currentPresetName == "guitar_xml")
        {
            ratioLocY = ratioLocY * 0.75f; // usable ratio for the guitar is 6/8 modules;
        }
        else if (currentPresetName == "BanjoLele_xml")
        {
            ratioLocY = ratioLocY * 0.66f; // usable ratio for the banjoLele is 4/6 modules;
        }
        audioMixer.SetFloat("mouseX", ratioLocX);
        audioMixer.SetFloat("mouseY", ratioLocY);
        Debug.Log(ratioLocX);

    }

    [DllImport("audioPlugin_ModularVST", CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr getPresetAt(int i);

}
