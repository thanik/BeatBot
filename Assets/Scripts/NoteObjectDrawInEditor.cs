using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NoteObjectDrawInEditor : MonoBehaviour
{
    public float yOffset;
    private NoteObject thisNote;
    private Rail thisRail;
    // Start is called before the first frame update
    void Start()
    {
        thisNote = GetComponent<NoteObject>();
        if (thisNote)
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

        if (Application.isEditor && !Application.isPlaying)
        {
            if (thisNote.time == 0f)
            {
                Debug.LogWarning("A note named \"" + thisNote.name + "\" on rail named " + thisRail.name + " has time value at 0.", this);
            }
            else
            {
                Vector3 calculatedPos = Vector3.Lerp(thisRail.transform.position, thisRail.endPosition, (thisNote.time - thisRail.startTime) / (thisRail.endTime - thisRail.startTime));
                calculatedPos.y += yOffset;
                transform.position = calculatedPos;

            }
        }
    }
}
