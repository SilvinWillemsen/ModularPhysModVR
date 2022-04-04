using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ExciterInteractionOnTrigger : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    private float excitationTypeValue;
    // Start is called before the first frame update

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
        audioMixer.SetFloat("excite", 1.0f);

        Debug.Log (excitationType);
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
        audioMixer.SetFloat("excite", 0.0f);

    }

}
