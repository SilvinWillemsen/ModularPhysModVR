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

    RectTransform icon; 

    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        bool resultX = audioMixer.GetFloat("MouseX", out mouseX);
        if (resultX)
        {
            mousePosition.x = mouseX * 10.0f; 
        }

        bool resultY = audioMixer.GetFloat("MouseX", out mouseY);
        if (resultY)
        {
            mousePosition.y = mouseY * 10.0f;
        }

        icon.anchoredPosition = mousePosition;
    }
}
