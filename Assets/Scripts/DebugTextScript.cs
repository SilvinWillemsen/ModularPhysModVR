using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class DebugTextScript : MonoBehaviour
{   
    private int timer = 0;
    public Text debugText;
    void Start()
    {
        debugText = this.GetComponent<Text>();
        debugText.text = "";

    }
    // Update is called once per frame
    void Update()
    {
            
        if (timer >= 0)
        {
            ++timer;
        }

        if (timer > 100)
        {
            debugText.text = "";
            timer = -1;
        }
    }
    public void setDebugText (string debugTextToSet)
    {
        debugText.text = debugTextToSet;
        timer = 0;
    }
        
    [DllImport("audioPlugin_ModularVST")]
    static extern int getNumPresets();

}
