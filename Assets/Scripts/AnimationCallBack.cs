using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCallBack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDespawn()
    {
        gameObject.transform.localScale = new Vector3(1e-5f, 1e-5f, 1e-5f);
    }

    public void OnSpawn()
    {
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.parent.GetComponent<Rigidbody>().useGravity = true;
    }
}
