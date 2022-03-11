using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class InstrumentInteractionOnTrigger : MonoBehaviour
{

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] GameObject instrumentGameObject; 
    [SerializeField] GameObject pickGameObject;
    [SerializeField] GameObject bowGameObject;
    [SerializeField] bool isVertical;               // strings are vertical

    private float excitationType;

    Collider instrumentCollider;

    // Start is called before the first frame update
    void Start()
    {
        pickGameObject.name = "Pick";
        bowGameObject.name = "Bow";
        instrumentCollider = instrumentGameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        audioMixer.SetFloat("exite", 1.0f);

        Debug.Log(other.gameObject.name);
        Debug.Log(pickGameObject.name);
        if (other.gameObject.name == pickGameObject.name)
        {
            
            excitationType = 0.1f;
        }

        if (other.gameObject.name == bowGameObject.name)
        {
            excitationType = 0.76f;
        }

        audioMixer.SetFloat("excitationType", excitationType);
    }
    private void OnTriggerStay(Collider other)
    {
        
        
        if (other.gameObject.name == pickGameObject.name || other.gameObject.name == bowGameObject.name)
        {
            Debug.Log("Excited");
            // map x and y
            Vector3 collisionLoc = other.transform.position;
            Vector3 relLoc = collisionLoc - instrumentGameObject.transform.position;

            // calculate where on the collider it is hit:
            float xPos = relLoc.x + instrumentCollider.bounds.size.x / 2;
            float yPos = relLoc.y + instrumentCollider.bounds.size.y / 2;

            if (!isVertical) // strings are horizontal, normal mapping
            {
                audioMixer.SetFloat("mouseX", xPos);
                audioMixer.SetFloat("mouseY", yPos);
            }

            if (isVertical) // strings are vertical, swap x and y
            {
                audioMixer.SetFloat("mouseX", yPos);
                audioMixer.SetFloat("mouseY", xPos);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        audioMixer.SetFloat("exite", 0.0f);
    }
}
