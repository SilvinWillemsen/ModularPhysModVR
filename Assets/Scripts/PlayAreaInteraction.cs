using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
/*
 Make sure that you scale the playarea transform, NOT the collider! That should have a scale of 1,1,1.
 */


public class PlayAreaInteraction : MonoBehaviour
{
    public enum StringOrientation
    {
        Horizontal,
        Vertical,
    };

    [SerializeField] AudioMixer audioMixer;
    [HideInInspector] public String instrumentType;

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

    public void SetInstrumentType(String instrumentTypeToSet)
    {
        Debug.Log("instrumentTypeToSet is " + instrumentTypeToSet);
        instrumentType = instrumentTypeToSet;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ExciterArea")
        {

            // calculate where on the instrument it's exciting:
            excitationLoc.transform.position = other.transform.position;

            Vector3 localPos = new Vector3(excitationLoc.transform.localPosition.x, excitationLoc.transform.localPosition.y, excitationLoc.transform.localPosition.z);


            // map & limit values, swap value for juce (Silvin: turns out it's always from -0.5 to 0.5)
            float xPos = Global.Limit(Global.Map(localPos.x, -0.5f, 0.5f, 0, 1), 0, 1);
            float yPos = 1.0f - Global.Limit(Global.Map(localPos.y, -0.5f, 0.5f, 0, 1), 0, 1);

            // Debug.Log(Global.Map(localPos.x, -xBounds / 2, xBounds / 2, 0, 1) + " " + Global.Map(localPos.y, -yBounds / 2, yBounds / 2, 0, 1));



            // Hard-coded mappings of the play area to the x and y positions
            double[] yVecSq = {0.0, 0.0, 0.0};
            double[] yVec = {xPos, yPos, 1.0};
            switch (instrumentType)
            {
                case "Guitar":
                    yPos = 0.75f * yPos;
                    float range = 0.1f;
                    float yPosPre = yPos;
                    yPos = Global.Limit(Global.Map(yPos, 0, 0.75f, -(1.0f - xPos) * range, 0.75f + (1.0f - xPos) * range), 0, 1);
                    break;
                case "Harp":
                    xPos = 1.0f - xPos;
                    // if (yPos > 0.6916)
                    // {
                    //     Matrix33d m; // matrix 
                    //     m(0, 0) = 15.6548;
                    //     m(0, 1) = 0.0;
                    //     m(0, 2) = 0.0;
                    //     m(1, 0) = 10.1353;
                    //     m(1, 1) = 1.0;
                    //     m(1, 2) = 0.0;
                    //     m(2, 0) = 14.6548;
                    //     m(2, 1) = 0.0;
                    //     m(2, 2) = 1.0;

                    //     double det = m(0, 0) * (m(1, 1) * m(2, 2) - m(2, 1) * m(1, 2)) -
                    //         m(0, 1) * (m(1, 0) * m(2, 2) - m(1, 2) * m(2, 0)) +
                    //         m(0, 2) * (m(1, 0) * m(2, 1) - m(1, 1) * m(2, 0));

                    //     double invdet = 1 / det;

                    //     Matrix33d minv; // inverse of matrix m
                    //     minv(0, 0) = (m(1, 1) * m(2, 2) - m(2, 1) * m(1, 2)) * invdet;
                    //     minv(0, 1) = (m(0, 2) * m(2, 1) - m(0, 1) * m(2, 2)) * invdet;
                    //     minv(0, 2) = (m(0, 1) * m(1, 2) - m(0, 2) * m(1, 1)) * invdet;
                    //     minv(1, 0) = (m(1, 2) * m(2, 0) - m(1, 0) * m(2, 2)) * invdet;
                    //     minv(1, 1) = (m(0, 0) * m(2, 2) - m(0, 2) * m(2, 0)) * invdet;
                    //     minv(1, 2) = (m(1, 0) * m(0, 2) - m(0, 0) * m(1, 2)) * invdet;
                    //     minv(2, 0) = (m(1, 0) * m(2, 1) - m(2, 0) * m(1, 1)) * invdet;
                    //     minv(2, 1) = (m(2, 0) * m(0, 1) - m(0, 0) * m(2, 1)) * invdet;
                    //     minv(2, 2) = (m(0, 0) * m(1, 1) - m(1, 0) * m(0, 1)) * invdet;

                    //     yVecSq = minv * yVec;
                    //     yPos = yVecSq[1] / yVecSq[2];
                    // }
                    // else if (xPos > 0.1 && yPos <= 0.6916)
                    // {
                    //     Matrix33d m; // matrix 
                    //     m(0, 0) = 15.6548;
                    //     m(0, 1) = 0.0;
                    //     m(0, 2) = 0.0;
                    //     m(1, 0) = 10.1353;
                    //     m(1, 1) = 1.0;
                    //     m(1, 2) = 0.0;
                    //     m(2, 0) = 14.6548;
                    //     m(2, 1) = 0.0;
                    //     m(2, 2) = 1.0;

                    //     double det = m(0, 0) * (m(1, 1) * m(2, 2) - m(2, 1) * m(1, 2)) -
                    //         m(0, 1) * (m(1, 0) * m(2, 2) - m(1, 2) * m(2, 0)) +
                    //         m(0, 2) * (m(1, 0) * m(2, 1) - m(1, 1) * m(2, 0));

                    //     double invdet = 1 / det;

                    //     Matrix33d minv; // inverse of matrix m
                    //     minv(0, 0) = (m(1, 1) * m(2, 2) - m(2, 1) * m(1, 2)) * invdet;
                    //     minv(0, 1) = (m(0, 2) * m(2, 1) - m(0, 1) * m(2, 2)) * invdet;
                    //     minv(0, 2) = (m(0, 1) * m(1, 2) - m(0, 2) * m(1, 1)) * invdet;
                    //     minv(1, 0) = (m(1, 2) * m(2, 0) - m(1, 0) * m(2, 2)) * invdet;
                    //     minv(1, 1) = (m(0, 0) * m(2, 2) - m(0, 2) * m(2, 0)) * invdet;
                    //     minv(1, 2) = (m(1, 0) * m(0, 2) - m(0, 0) * m(1, 2)) * invdet;
                    //     minv(2, 0) = (m(1, 0) * m(2, 1) - m(2, 0) * m(1, 1)) * invdet;
                    //     minv(2, 1) = (m(2, 0) * m(0, 1) - m(0, 0) * m(2, 1)) * invdet;
                    //     minv(2, 2) = (m(0, 0) * m(1, 1) - m(1, 0) * m(0, 1)) * invdet;

                    //     yVecSq = minv * yVec;
                    //     yPos = yVecSq[1] / yVecSq[2];
                    // }
                    break;
                    case "BanjoLele":
                        yPos = 0.66f * yPos;
                        break;
                default:
                    Debug.LogError("Please set instrumenttype"); ;
                    break;
            }
            // Map according to the string orientation
            // Flip x and y positions if vertical
            audioMixer.SetFloat("mouseX", stringOrientation == StringOrientation.Vertical ? yPos : xPos);
            audioMixer.SetFloat("mouseY", stringOrientation == StringOrientation.Vertical ? xPos : yPos);


            // visual representation
            //excitelocIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos * 10, yPos * 10);

        }
    }


}
