using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PickScript : MonoBehaviour
{
    public GameObject playArea;

    public AudioMixer audioMixer;
    public Text debugText;
    public ChangePreset presetController;
    private int curExcitationType;
    private float smoothness;
    private bool scrollForExcitation = true;

    private Vector3 curPos, prevPos;
    private float curYvel;

    // Start is called before the first frame update
    Collider m_Collider;
    float m_Min_x, m_Max_x, m_Min_y, m_Max_y, x_range, y_range;
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
        if (Input.mouseScrollDelta.y != 0) 
        {
            if (Input.mouseScrollDelta.y < 0.0f)
            {
                if (scrollForExcitation)
                {
                    if (curExcitationType < 2)
                    {
                        curExcitationType++;
                    }
                } else {
                    smoothness += 0.1f;
                    if (smoothness > 1.0f)
                    {
                        smoothness = 1.0f;
                    }
                }
                Debug.Log("scroll up");

            } 
            else if (Input.mouseScrollDelta.y > 0.0f) 
            {

                if (scrollForExcitation)
                {
                    if (curExcitationType > 0)
                    {
                        curExcitationType--;
                    }
                } else {
                    smoothness -= 0.1f;
                    if (smoothness < 0.0f)
                    {
                        smoothness = 0.0f;
                    }
                }
                Debug.Log("scroll down");
            }
            if (scrollForExcitation)
            {
                audioMixer.SetFloat("excitationType", curExcitationType * 0.33f + 0.1f);
                string excitationTypeString= "Excitation: ";
                switch(curExcitationType)
                {
                    case 0:
                        excitationTypeString += "Pluck";
                        break;
                    case 1:
                        excitationTypeString += "Hammer";
                        break;
                    case 2:
                        excitationTypeString += "Bow";
                        break;
                    default:
                        excitationTypeString = "";
                        break;
                }
                debugText.GetComponent<DebugTextScript>().setDebugText (excitationTypeString);

            } else {
                audioMixer.SetFloat("smoothness", smoothness);

                debugText.GetComponent<DebugTextScript>().setDebugText ("Smoothness = " + smoothness);

            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            if (curExcitationType == 1)
                StartCoroutine(triggerHammer());
            else
                debugText.GetComponent<DebugTextScript>().setDebugText ("Not exciting with hammer");

        }

    }

    private void FixedUpdate() {
        curPos = transform.position;
        curYvel = Mathf.Abs((curPos.magnitude - prevPos.magnitude) / Time.fixedDeltaTime);
        prevPos = curPos;
        audioMixer.SetFloat ("hammerVelocity", curYvel / 20.0f);
        // Debug.Log(curYvel);

    }
    IEnumerator triggerHammer()
    {
        audioMixer.SetFloat ("trigger", 1.0f);
        yield return new WaitForSeconds (0.1f);

        audioMixer.SetFloat ("trigger", 0.0f);
        debugText.GetComponent<DebugTextScript>().setDebugText ("Hammer triggered");
    }

    void OnTriggerEnter (Collider other) {
        Debug.Log(curExcitationType);
        if (curExcitationType == 1)
        {
            StartCoroutine(triggerHammer());
            // Debug.Log("triggerHammer");
        }

    }


    void OnTriggerStay (Collider other) {

        string currentPresetName = Marshal.PtrToStringAuto(getPresetAt(presetController.currentlyActivePreset));

        float ratioLocX = (transform.position.x - m_Min_x) / x_range;
        float ratioLocY = (1.0f - (transform.position.y - m_Min_y) / y_range);
        if (currentPresetName == "guitar_xml")
        {
            ratioLocY = ratioLocY * 0.75f; // usable ratio for the guitar is 6/8 modules;
        } else if (currentPresetName == "BanjoLele_xml")
        {
            ratioLocY = ratioLocY * 0.66f; // usable ratio for the banjoLele is 4/6 modules;
        }
        audioMixer.SetFloat("mouseX", ratioLocX);
        audioMixer.SetFloat("mouseY", ratioLocY);
        Rigidbody test = other.gameObject.GetComponent<Rigidbody>();
        // RigidBody test = GetComponent<RigidBody>();

    }

    [DllImport("audioPlugin_ModularVST", CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr getPresetAt (int i);

}
