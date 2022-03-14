using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public class ControlPanelScript : MonoBehaviour
{
    public GameObject stringObject;
    public GameObject twoDObject;
    public GameObject instrumentPanel;
    private List<GameObject> resonatorModuleList;

    private List<int> objectTypes;
    private int numStrings = 0;

    private float centerOfPanelX, centerOfPanelY, centerOfPanelZ; 
    private float panelHeight;
    private Vector3 newScale;
    void Start()
    {
        resonatorModuleList = new List<GameObject>();
        objectTypes = new List<int>();
        centerOfPanelX = instrumentPanel.transform.position.x;
        centerOfPanelY = instrumentPanel.transform.position.y;
        centerOfPanelZ = instrumentPanel.transform.position.z;

        panelHeight = instrumentPanel.GetComponent<RectTransform>().rect.height;

    }   
    public void AddString()
    {
        GameObject newString = Instantiate (stringObject, stringObject.transform.position, stringObject.transform.rotation) as GameObject;
        newString.SetActive (true);
        newString.transform.parent = instrumentPanel.transform;

        resonatorModuleList.Add (newString); 
        objectTypes.Add (0); 
        Debug.Log("AddString");

        RearrangeModules();
    }

    public void AddTwoDObject()
    {
        GameObject newTwoDObject = Instantiate (twoDObject, twoDObject.transform.position, twoDObject.transform.rotation) as GameObject;
        newTwoDObject.SetActive (true);
        newTwoDObject.transform.parent = instrumentPanel.transform;
        
        resonatorModuleList.Add (newTwoDObject); 
        objectTypes.Add (1); 

        Debug.Log("AddTwoDObject with newScale = " + newScale);
        RearrangeModules();
    }

    private void RearrangeModules()
    {
        newScale = new Vector3 (25.0f * twoDObject.GetComponent<Transform>().localScale.x, 
                                twoDObject.GetComponent<Transform>().localScale.y,
                                40.0f / (resonatorModuleList.Count + 1));

        float increment = panelHeight / resonatorModuleList.Count;
        float curY = -increment * 0.5f;
        for (int i = 0; i < resonatorModuleList.Count; ++i)
        {        
            Vector3 objectPos = new Vector3 (centerOfPanelX, 
                                                curY + panelHeight * 0.5f,
                                                centerOfPanelZ - 10.0f);
            if (objectTypes[i] == 1)
            {
                resonatorModuleList[i].GetComponent<Transform>().localScale = newScale; 
                Debug.Log("changescale");
            }
            resonatorModuleList[i].GetComponent<Transform>().localPosition = objectPos;
            Debug.Log(objectPos);
            curY -= increment;
        }
    }

    public void EditConnections()
    {
        Debug.Log("Edit Connections");
    }

    public void PrintPreset()
    {
        // string xmlPreset = Marshal.PtrToStringAuto (getXMLOfPresetAt(0));
        // Debug.Log(xmlPreset);
    }


}
