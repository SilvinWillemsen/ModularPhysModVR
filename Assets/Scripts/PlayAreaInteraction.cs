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

    private float hammerVelocity;

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

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "ExciterArea" && other.transform.parent.name == "Hammer")
        {
            hammerVelocity = Global.Limit (other.transform.parent.GetComponent<HammerVelocityTracker>().getVelocity().magnitude / 4.0f, 0.0f, 1.0f);
            StartCoroutine(triggerHammer());
        }
    }

    IEnumerator triggerHammer()
    {
        Debug.Log (hammerVelocity);
        audioMixer.SetFloat("hammerVelocity", hammerVelocity);
        yield return new WaitForSeconds(0.1f);

        audioMixer.SetFloat("trigger", 1.0f);
        yield return new WaitForSeconds(0.1f);

        audioMixer.SetFloat("trigger", 0.0f);
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
                    //float yPosN = xPos; 
                    yPos = 1 - yPos;
                    xPos = xPos;
                    if (yPos > 0.7419)
                    {
                        double[,] m = new double[3, 3] { { 10.6214, 0.0, 0.0 }, { 7.1381, 1.0, 0.0 }, { 9.6214, 0.0, 1.0 } };

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
                        //if (yPos > 1 || xPos > 1)
                        //{
                        //    yPos = 0;
                        //    xPos = 0;
                        //}
                        Debug.Log("calculated area B");
                    }
                    else if (xPos > 0.1328 && yPos <= 0.7419)
                    {
                        double[,] m = new double[3,3] { { -8.9858, 0.0, 1.1706 }, { -6.5399, -0.1706, 0.8685 }, { -8.8151, 0.0, 1.0 } };


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
                        //if (yPos > 1 || xPos > 1)
                        //{
                        //    yPos = 0;
                        //    xPos = 0;
                        //}
                        Debug.Log("calculated area C");
                    }
                    else Debug.Log("calculated area A");
                    break;
                    case "BanjoLele":
                        yPos = 0.66f * yPos;
                        break;
                    case "Timpani":
                        Debug.Log (xPos + " " + yPos);
                        break;
                    case "Marimba":
                        break;
                default:
                    Debug.Log("Instrument Type is currently " + instrumentType);
                    Debug.LogWarning("No custom playarea defined");
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
