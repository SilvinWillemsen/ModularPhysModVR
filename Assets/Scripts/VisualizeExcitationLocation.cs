using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VisualizeExcitationLocation : MonoBehaviour
{
    [SerializeField]AudioMixer audioMixer;

    private float mouseX;
    private float mouseY;

    Vector3 mousePosition = Vector3.zero;
    Vector3 startPos;
    RectTransform icon; 

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        icon = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        bool resultX = audioMixer.GetFloat("mouseX1", out mouseX);
        if (resultX)
        {
            mousePosition.x = mouseX * 10.0f; 
        }
        bool resultY = audioMixer.GetFloat("mouseY1", out mouseY);
        if (resultY)
        {
            mousePosition.y = mouseY * 10.0f;
        }

        icon.anchoredPosition = mousePosition;
    }
}
