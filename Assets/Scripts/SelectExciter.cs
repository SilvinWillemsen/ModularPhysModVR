using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SelectExciter : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    private float excitationTypeValue;
    // Start is called before the first frame update

    // Three excitation types
    public enum ExcitationType
    {
        Pick,
        Hammer,
        Bow
    }
    
    [SerializeField] ExcitationType excitationType;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExciterGrabbed()
    {
        Debug.Log("ExciterGrabbed");

        // Turn on excite in the plugin once exciter is grabbed
        audioMixer.SetFloat("excite", 1.0f);

        Debug.Log (excitationType);

        // Choose what excitation type to set the plugin to based on the dropdown menu in the inspector
        switch (excitationType)
        {
            case ExcitationType.Pick:
                excitationTypeValue = 0.1f;
                break;
            case ExcitationType.Hammer:
                excitationTypeValue = 0.5f;
                break;
            case ExcitationType.Bow:
                excitationTypeValue = 0.76f;
                break;
        }

        audioMixer.SetFloat("excitationType", excitationTypeValue);
    }

    public void ExciterReleased()
    {
        // Turn off excite in the plugin once exciter is released
        audioMixer.SetFloat("excite", 0.0f);
    }

}