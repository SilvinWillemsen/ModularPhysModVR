using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{   
    public Camera camera;
    private float curXVal, xValToGoTo, curYVal, yValToGoTo;
    private bool mouseControl = false;
    private bool rayCastControl = true;
    public GameObject guitar;
    private float guitarColliderMinX, guitarColliderMaxX, guitarColliderRangeX, guitarColliderMinY, guitarColliderMaxY, guitarColliderRangeY;
    void Start()
    {
        // get collider limits for guitar
        guitarColliderMinX = guitar.GetComponent<Collider>().bounds.min.x;
        guitarColliderMaxX = guitar.GetComponent<Collider>().bounds.max.x;
        guitarColliderRangeX = guitarColliderMaxX - guitarColliderMinX;

        guitarColliderMinY = guitar.GetComponent<Collider>().bounds.min.y;
        guitarColliderMaxY = guitar.GetComponent<Collider>().bounds.max.y;
        guitarColliderRangeY = guitarColliderMaxY - guitarColliderMinY;

        curXVal = this.gameObject.transform.position.x;
        curYVal = this.gameObject.transform.position.y;
        xValToGoTo = curXVal;
        yValToGoTo = curYVal;
        print("Min X = " + guitarColliderMinX);
        print("Max X = " + guitarColliderMaxX);
        print("curXVal = " + curXVal);

        print("Min Y = " + guitarColliderMinY);
        print("Max Y = " + guitarColliderMaxY);
        print("curYVal = " + curYVal);

    }
    // Update is called once per frame
    void Update()
    {
        if (mouseControl)
        {
            this.gameObject.transform.position = new Vector3 (
                guitarColliderMinX + guitarColliderRangeX * Input.mousePosition.x / Screen.width, 
                guitarColliderMinY + guitarColliderRangeY * Input.mousePosition.y / Screen.height, 
                this.gameObject.transform.position.z);
            // Debug.Log("MouseX: " + Input.mousePosition.x / Screen.width);
            // Debug.Log("MouseY: " + Input.mousePosition.y / Screen.height);
        } else if (rayCastControl)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay (Input.mousePosition);
        
            if (Physics.Raycast(ray, out hit)) {
                Transform objectHit = hit.transform;
                if (objectHit == guitar.transform)
                { 
                    this.gameObject.transform.position = new Vector3 (hit.point.x, hit.point.y, this.gameObject.transform.position.z);
                                Debug.Log(this.gameObject.transform.position);
                }

            }
        } else {
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 mousePos = Input.mousePosition;
                {
                    Debug.Log(mousePos.x);
                    Debug.Log(mousePos.y);
                }
            }

            if (Input.GetKeyDown("left"))
            {
                xValToGoTo -= 0.1f;
                if (xValToGoTo <= guitarColliderMinX)
                    xValToGoTo = guitarColliderMinX;
                print("left");
            } 

            if (Input.GetKeyDown("right"))
            {	    
                xValToGoTo += 0.1f;
                if (xValToGoTo >= guitarColliderMaxX)
                    xValToGoTo = guitarColliderMaxX;	
                print("right");
            }



            if (Input.GetKeyDown("down"))
            {
                yValToGoTo -= 0.1f;
                if (yValToGoTo <= guitarColliderMinY)
                    yValToGoTo = guitarColliderMinY;
                print("down");
            } 

            if (Input.GetKeyDown("up"))
            {	    
                yValToGoTo += 0.1f;
                if (yValToGoTo >= guitarColliderMaxY)
                    yValToGoTo = guitarColliderMaxY;	
                print("up");
            }
            // Smoothing happens inside the plugin
            // curXVal = 0.95f * curXVal + 0.05f * xValToGoTo;
            // curYVal = 0.95f * curYVal + 0.05f * yValToGoTo;

            this.gameObject.transform.position = new Vector3 (curXVal, curYVal, this.gameObject.transform.position.z);
        }
    }


}
