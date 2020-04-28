using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RailDrawInEditor : MonoBehaviour
{
    public bool ShowInEditor;
    public float speed;
    private Rail thisRail;
    // Start is called before the first frame update
    void Start()
    {
        thisRail = GetComponent<Rail>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 endPosition = new Vector3(transform.position.x + (thisRail.movingSpeed * (thisRail.endTime - thisRail.startTime)), transform.position.y);
        //thisRail.endPosition = endPosition;
        if (transform.position.y != thisRail.endPosition.y)
        {
            thisRail.endPosition.y = transform.position.y;
        }

        if (thisRail.startTime == 0f && thisRail.endTime == 0f)
        {
            Debug.LogWarning("A rail named \"" + thisRail.name + "\" has start and end time value at 0.", this);
        }
        else
        {
            speed = (thisRail.endPosition.x - transform.position.x) / (thisRail.endTime - thisRail.startTime);
        }
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
