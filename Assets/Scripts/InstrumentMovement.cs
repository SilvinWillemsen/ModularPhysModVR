using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentMovement : MonoBehaviour
{

    private Vector3 startPos;
    private Quaternion startRot;
    
    private bool isMoving = false;
    
    private Vector3 currentPos;
    private Quaternion currentRot;

    [SerializeField] private float lerpDuration = 1.0f;
    private float timeElapsed; 

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startRot = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (timeElapsed < lerpDuration)
            {

                transform.position = Vector3.Lerp(currentPos, startPos, timeElapsed / lerpDuration);
                transform.rotation = Quaternion.Lerp(currentRot, startRot, timeElapsed / lerpDuration);

                timeElapsed += Time.deltaTime;

            }
            else
            {
                transform.position = startPos;
                transform.rotation = startRot; 

                isMoving = false;
            } 
        }
    }

    public void ResetPosition()
    {
        timeElapsed = 0;

        isMoving = true;

        currentPos = transform.position; 
        currentRot = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }


}
