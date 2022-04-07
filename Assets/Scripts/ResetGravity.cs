using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGravity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnGravityOn()
    {
        gameObject.transform.parent.GetComponent<Rigidbody>().useGravity = true;
        Debug.Log("Turning On Gravity");
    }
}
