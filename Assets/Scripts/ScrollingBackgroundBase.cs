using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackgroundBase : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 position;
    void Start()
    {
        mainCam = Camera.main;
        position = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //position.x = mainCam.transform.position.x;
        position.y = -mainCam.transform.position.y;
        transform.localPosition = position;
    }
}
