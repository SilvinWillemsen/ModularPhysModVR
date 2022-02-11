using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PickScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    private int curExcitationType;
    // Start is called before the first frame update
    Collider m_Collider;
    float m_Min_x, m_Max_x, m_Min_y, m_Max_y, x_range, y_range;
    void Start()
    {
        m_Collider = GetComponent<Collider>();
        //Fetch the size of the Collider volume
        //Fetch the minimum and maximum bounds of the Collider volume
        m_Min_x = m_Collider.bounds.min.x;
        m_Max_x = m_Collider.bounds.max.x;
        m_Min_y = m_Collider.bounds.min.y;
        m_Max_y = m_Collider.bounds.max.y;
        x_range = m_Max_x - m_Min_x;
        y_range = m_Max_y - m_Min_y;
        float outFloat; 
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

        OutputData();
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
        if (Input.mouseScrollDelta.y < 0.0f)
        {
            if (curExcitationType < 2)
            {
                curExcitationType++;
            }
            Debug.Log("scroll up");
        } 
        else if (Input.mouseScrollDelta.y > 0.0f) 
        {
            if (curExcitationType > 0)
            {
                curExcitationType--;
            }
            Debug.Log("scroll down");
        }
        audioMixer.SetFloat("excitationType", curExcitationType * 0.33f + 0.1f);

    }
    // void OnTriggerEnter(Collider other) {
    //     print("Points colliding: " + other.contacts.Length);
    //     print("First point that collided: " + other.contacts[0].point);
    //     // print("Another object has entered the trigger");  
    // }  
    
    // void OnCollisionStay(Collision collision)
    // {
    //     foreach (ContactPoint contact in collision.contacts)
    //     {
    //         print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
    //         // Visualize the contact point
    //         Debug.DrawRay(contact.point, contact.normal, Color.white);
    //     }
    // }


    void OnTriggerStay(Collider other) {
        float ratioLocX = (other.gameObject.transform.position.x - m_Min_x) / x_range;
        float ratioLocY = (1.0f - (other.gameObject.transform.position.y - m_Min_y) / y_range);
        ratioLocY = ratioLocY * 0.75f; // usable ratio for the guitar is 6/8 modules;
        // print("Ratio X: " + ratioLocX);
        // print("Ratio Y: " + ratioLocY);
        audioMixer.SetFloat("mouseX", ratioLocX);
        audioMixer.SetFloat("mouseY", ratioLocY);

    }
}
