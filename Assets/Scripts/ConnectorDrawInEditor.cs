using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ConnectorDrawInEditor : MonoBehaviour
{
    private Connector thisConnector;
    private Rail thisRail;
    // Start is called before the first frame update
    void Start()
    {
        thisConnector = GetComponent<Connector>();
        if (thisConnector)
        {
            thisRail = GetComponentInParent<Rail>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<Rail>() != thisRail)
        {
            thisRail = GetComponentInParent<Rail>();
        }

        if (thisConnector.startTime == 0f && thisConnector.endTime == 0f)
        {
            Debug.LogWarning("A connector named \"" + thisConnector.name + "\" on rail named " + thisRail.name + " has start and end time value at 0.", this);
        }
        else
        {
            transform.position = Vector3.Lerp(thisRail.transform.position, thisRail.endPosition, (thisConnector.startTime - thisRail.startTime) / (thisRail.endTime - thisRail.startTime));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.75f);
        Gizmos.DrawCube(transform.position, Vector3.one * 0.35f);

        // TODO: should calculate position on rail based on endTime
        if (thisConnector)
        {
            if (thisConnector.pressedAction == ConnectorActionEnum.JUMP_TO_RAIL && thisConnector.pressedToRail)
            {
                Gizmos.color = new Color(0, 1, 0, 0.5f);
                //Gizmos.DrawLine(transform.position, thisConnector.pressedToRail.transform.position);
                Rail destination = thisConnector.pressedToRail;
                Gizmos.DrawLine(transform.position, Vector3.Lerp(destination.transform.position, destination.endPosition, (thisConnector.endTime - destination.startTime) / (destination.endTime - destination.startTime)));
            }

            if (thisConnector.unpressedAction == ConnectorActionEnum.JUMP_TO_RAIL && thisConnector.unpressedToRail)
            {
                Gizmos.color = new Color(1, 0, 0, 0.5f);
                Rail destination = thisConnector.unpressedToRail;
                float endTime = thisConnector.unpressedEndTime > 0 ? thisConnector.unpressedEndTime : thisConnector.endTime;
                Gizmos.DrawLine(transform.position, Vector3.Lerp(destination.transform.position, destination.endPosition, (endTime - destination.startTime) / (destination.endTime - destination.startTime)));
                foreach (Vector3 additionalPos in thisConnector.additionalUnpressedPositionCurve)
                {
                    Gizmos.DrawSphere(additionalPos, 0.25f);
                }
                //Gizmos.DrawLine(transform.position, thisConnector.unpressedToRail.transform.position);
            }
        }
    }
}
