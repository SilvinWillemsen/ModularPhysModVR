using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerVelocityTracker : MonoBehaviour
{

    [SerializeField] private GameObject hammerInteractable;

    private Vector3 prevPosition;
    private Vector3 curVelocity;

    // Start is called before the first frame update
    void Start()
    {
        prevPosition = hammerInteractable.transform.position;
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        curVelocity = (hammerInteractable.transform.position - prevPosition) / Time.fixedDeltaTime;
        prevPosition = hammerInteractable.transform.position;
    }

    public Vector3 getVelocity()
    {
        return curVelocity;
    }
}
