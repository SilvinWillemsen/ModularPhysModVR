using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowPosTracker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayArea")
            transform.GetChild(0).transform.position = other.transform.GetComponent<Collider>().ClosestPoint(transform.position);

    }
}
