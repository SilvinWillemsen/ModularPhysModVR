using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayAreaInteraction : MonoBehaviour
{
    public enum StringOrientation
    {
        Horizontal,
        Vertical,
    };

    [SerializeField] AudioMixer audioMixer;
    [HideInInspector] public string instrumentType;

    private GameObject excitationLoc;
    [SerializeField] private StringOrientation stringOrientation;



    // Start is called before the first frame update
    void Start()
    {        
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

            if (instrumentType == "Harp")
            {
                Debug.Log("Harp is reached");
            } else {
                // Flip x and y positions if vertical
                audioMixer.SetFloat("mouseX", stringOrientation == StringOrientation.Vertical ? yPos : xPos);
                audioMixer.SetFloat("mouseY", stringOrientation == StringOrientation.Vertical ? xPos : yPos);
            }

            // visual representation
            //excitelocIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos * 10, yPos * 10);

        }
    }


}
