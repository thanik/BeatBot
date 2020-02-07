﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RailDrawInEditor : MonoBehaviour
{
    public bool ShowInEditor;

    private Rail thisRail;
    // Start is called before the first frame update
    void Start()
    {
        thisRail = GetComponent<Rail>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 endPosition = new Vector3(transform.position.x + (thisRail.movingSpeed * (thisRail.endTime - thisRail.startTime)), transform.position.y);
        thisRail.endPosition = endPosition;
    }

    private void OnDrawGizmos()
    {
        if (thisRail)
        {
            
            if (ShowInEditor)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, thisRail.endPosition);
            }
        }
    }
}