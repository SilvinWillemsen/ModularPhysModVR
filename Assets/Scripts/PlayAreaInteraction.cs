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

            double[] yVec = new double[3] { xPos, yPos, 1.0 };
            double[] locOnSquare = new double[3];
            
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
                    if (yPos > 0.6916)
                    {
                        double[,] m = new double[3, 3] { { 15.6548, 0.0, 0.0 }, { 10.1353, 1.0, 0.0 }, { 14.6548, 0.0, 1.0 } };

                        double det = m[0,0] * (m[1,1] * m[2,2] - m[2,1] * m[1,2]) -
                            m[0,1] * (m[1,0] * m[2,2] - m[1,2] * m[2,0]) +
                            m[0,2] * (m[1,0] * m[2,1] - m[1,1] * m[2,0]);

                        double invdet = 1 / det;
                        
                        double[,] minv= new double[3,3]; // inverse of matrix m
                        minv[0,0] = (m[1,1] * m[2,2] - m[2,1] * m[1,2]) * invdet;
                        minv[0,1] = (m[0,2] * m[2,1] - m[0,1] * m[2,2]) * invdet;
                        minv[0,2] = (m[0,1] * m[1,2] - m[0,2] * m[1,1]) * invdet;
                        minv[1,0] = (m[1,2] * m[2,0] - m[1,0] * m[2,2]) * invdet;
                        minv[1,1] = (m[0,0] * m[2,2] - m[0,2] * m[2,0]) * invdet;
                        minv[1,2] = (m[1,0] * m[0,2] - m[0,0] * m[1,2]) * invdet;
                        minv[2,0] = (m[1,0] * m[2,1] - m[2,0] * m[1,1]) * invdet;
                        minv[2,1] = (m[2,0] * m[0,1] - m[0,0] * m[2,1]) * invdet;
                        minv[2,2] = (m[0,0] * m[1,1] - m[1,0] * m[0,1]) * invdet;

                        locOnSquare[0] = minv[0,0] * yVec[0] + minv[1,0] * yVec[1] + minv[2,0] * yVec[2];
                        locOnSquare[1] = minv[0,1] * yVec[0] + minv[1,1] * yVec[1] + minv[2,1] * yVec[2];
                        locOnSquare[2] = minv[0,2] * yVec[0] + minv[1,2] * yVec[1] + minv[2,2] * yVec[2];

                        yPos = (float)(locOnSquare[1] / locOnSquare[2]);
                    }
                    else if (xPos > 0.1 && yPos <= 0.6916)
                    {
                        double[,] m = new double[3,3] { { -34.0625, 0.0, 3.1875 }, { -22.0448, -2.1875, 2.2045 }, { -31.8750, 0.0, 1.0 } };


                        double det = m[0,0] * (m[1,1] * m[2,2] - m[2,1] * m[1,2]) -
                            m[0,1] * (m[1,0] * m[2,2] - m[1,2] * m[2,0]) +
                            m[0,2] * (m[1,0] * m[2,1] - m[1,1] * m[2,0]);

                        double invdet = 1 / det;

                        double[,] minv = new double[3,3]; // inverse of matrix m
                        minv[0,0] = (m[1,1] * m[2,2] - m[2,1] * m[1,2]) * invdet;
                        minv[0,1] = (m[0,2] * m[2,1] - m[0,1] * m[2,2]) * invdet;
                        minv[0,2] = (m[0,1] * m[1,2] - m[0,2] * m[1,1]) * invdet;
                        minv[1,0] = (m[1,2] * m[2,0] - m[1,0] * m[2,2]) * invdet;
                        minv[1,1] = (m[0,0] * m[2,2] - m[0,2] * m[2,0]) * invdet;
                        minv[1,2] = (m[1,0] * m[0,2] - m[0,0] * m[1,2]) * invdet;
                        minv[2,0] = (m[1,0] * m[2,1] - m[2,0] * m[1,1]) * invdet;
                        minv[2,1] = (m[2,0] * m[0,1] - m[0,0] * m[2,1]) * invdet;
                        minv[2,2] = (m[0,0] * m[1,1] - m[1,0] * m[0,1]) * invdet;

                        locOnSquare[0] = minv[0,0] * yVec[0] + minv[1,0] * yVec[1] + minv[2,0] * yVec[2];
                        locOnSquare[1] = minv[0,1] * yVec[0] + minv[1,1] * yVec[1] + minv[2,1] * yVec[2];
                        locOnSquare[2] = minv[0,2] * yVec[0] + minv[1,2] * yVec[1] + minv[2,2] * yVec[2];

                        yPos = (float)(locOnSquare[1] / locOnSquare[2]);
                    }
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
