using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.InteropServices;

public class XMLparser : MonoBehaviour
{
    // public Text uiText;
    public Canvas canvas; 
    public void LoadPreset()
    {   
        Debug.Log("Loading preset: " + Marshal.PtrToStringAuto (getPresetAt(0)));
        string xmlRaw = Marshal.PtrToStringAuto (getXMLOfPresetAt(0));
        parseXmlFile (xmlRaw);
    }

    void parseXmlFile (string xmlData)
    {
        string totVal = "";
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));
        string xmlPathPattern = "//App/Instrument";
        XmlNodeList instruments = xmlDoc.SelectNodes(xmlPathPattern);
        Debug.Log(instruments[0].Attributes["id"].Value);
        foreach (XmlNode instrument in instruments)
        {
            XmlNodeList resonators = instrument.SelectNodes("descendant::Resonator");
            XmlNodeList connections = instrument.SelectNodes("descendant::Connection");
            foreach (XmlNode resonator in resonators)
            {
                Debug.Log("Resonator " + resonator.Attributes["id"].Value + " is of type " + resonator.Attributes["type"].Value);
                switch(resonator.Attributes["type"].Value)
                {
                    case "Stiff_String":
                        GetComponent<ControlPanelScript>().AddString();
                        break;
                    case "Bar":
                        GetComponent<ControlPanelScript>().AddString();
                        break;
                    case "Thin_Plate":
                        GetComponent<ControlPanelScript>().AddTwoDObject();
                        break;
                }

                // if (resonator.Attributes["type"].Value == Stiff_String)
                // else if (resonator.Attributes["type"].Value == Thin_Plate)

                // else if (resonator.Attributes["type"].Value == Thin_Plate)
            }   

            foreach (XmlNode connection in connections)
            {
                Debug.Log("Connection " + connection.Attributes["id"].Value);
            }
        }
    }
    [DllImport("audioPlugin_ModularVST")]
    static extern IntPtr getXMLOfPresetAt(int i);

    [DllImport("audioPlugin_ModularVST")]
    static extern IntPtr getPresetAt(int i);
}
