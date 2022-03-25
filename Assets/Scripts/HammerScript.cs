using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class HammerScript : MonoBehaviour
{
    public GameObject playArea;

    public AudioMixer audioMixer;
    public Text debugText;
    public ChangePreset presetController;
    private int curExcitationType;
    private float smoothness;
    private bool scrollForExcitation = true;

    private Vector3 curPos, prevPos;
    private float curVel;

    // Start is called before the first frame update
    Collider m_Collider;
    float m_Min_x, m_Max_x, m_Min_y, m_Max_y, x_range, y_range;

    private float ratioLocX, ratioLocY, curMouseY = 0.0f;
    void Start()
    {
        m_Collider = playArea.gameObject.GetComponent<Collider>();
        //Fetch the size of the Collider volume
        //Fetch the minimum and maximum bounds of the Collider volume
        m_Min_x = m_Collider.bounds.min.x;
        m_Max_x = m_Collider.bounds.max.x;
        m_Min_y = m_Collider.bounds.min.y;
        m_Max_y = m_Collider.bounds.max.y;
        x_range = m_Max_x - m_Min_x;
        y_range = m_Max_y - m_Min_y;
        float outFloat; 
        if (scrollForExcitation)
        {
            audioMixer.GetFloat ("excitationType", out outFloat);
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

        } else {
            audioMixer.GetFloat ("smoothness", out outFloat);
            smoothness = outFloat;
        }
        curPos = transform.position;
        prevPos = transform.position;
        // OutputData();
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
        audioMixer.SetFloat ("hammerVelocity", curVel * 0.1f);
    //    audioMixer.SetFloat ("hammerVelocity", 0.5f);

    }

    private void FixedUpdate() {
        curPos = transform.position;
        curVel = Mathf.Abs((curPos.magnitude - prevPos.magnitude) / Time.fixedDeltaTime);
        prevPos = curPos;
        // Debug.Log(curVel);
        // Debug.Log(curVel / 20.0f);

    }
    IEnumerator triggerHammer()
    {
        UpdateRatioLocs();
        audioMixer.SetFloat("mouseX", ratioLocX);
        audioMixer.SetFloat("mouseY", ratioLocY);
        yield return new WaitForSeconds (0.1f);
        audioMixer.SetFloat ("trigger", 1.0f);
        yield return new WaitForSeconds (0.1f);
        audioMixer.SetFloat ("trigger", 0.0f);
        debugText.GetComponent<DebugTextScript>().setDebugText ("Hammer triggered");
    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject == playArea.gameObject)
        {
            StartCoroutine(triggerHammer());
        } 
        // else 
        // {
        //     if (string1.gameObject == other.gameObject)
        //     {
        //         curMouseY = 0.0f + 0.01f;
        //         Debug.Log("string1");
        //     } 
        //     else if (string2.gameObject == other.gameObject)
        //     {
        //         curMouseY = 1.0f/6.0f * 0.75f + 0.01f;
        //         Debug.Log("string2");

        //     }
        //     else if (string3.gameObject == other.gameObject)
        //     {
        //         curMouseY = 2.0f/6.0f * 0.75f + 0.01f;
        //         Debug.Log("string3");
        //     }
        //     else if (string4 == other.gameObject)
        //     {
        //         curMouseY = 3.0f/6.0f * 0.75f + 0.01f;
        //         Debug.Log("string4");
        //     }
        //     else if (string5 == other.gameObject)
        //     {
        //         curMouseY = 4.0f/6.0f * 0.75f + 0.01f;
        //         Debug.Log("string5");
        //     }
        //     else if (string6 == other.gameObject)
        //     {
        //         curMouseY = 5.0f/6.0f * 0.75f + 0.01f;
        //         Debug.Log("string6");
        //     }
        //     StartCoroutine(triggerHammer());
        // }
    }


    void OnTriggerStay (Collider other) {
        // UpdateRatioLocs();
        // audioMixer.SetFloat("mouseX", ratioLocX);
        // audioMixer.SetFloat("mouseY", ratioLocY);
        // Debug.Log("updated mouseY");

    }

    void UpdateRatioLocs()
    {
        string currentPresetName = Marshal.PtrToStringAuto(getPresetAt(presetController.currentlyActivePreset));

        ratioLocX = (transform.position.x - m_Min_x) / x_range;
        ratioLocY = (1.0f - (transform.position.y - m_Min_y) / y_range);
        if (currentPresetName == "guitar_xml")
        {
            ratioLocY = ratioLocY * 0.75f; // usable ratio for the guitar is 6/8 modules;
        } else if (currentPresetName == "BanjoLele_xml")
        {
            ratioLocY = ratioLocY * 0.66f; // usable ratio for the banjoLele is 4/6 modules;
        }
    }

    [DllImport("audioPlugin_ModularVST", CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr getPresetAt (int i);

}
