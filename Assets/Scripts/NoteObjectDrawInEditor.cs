using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NoteObjectDrawInEditor : MonoBehaviour
{
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
        transform.position = Vector3.Lerp(thisRail.transform.position, thisRail.endPosition, (thisNote.time - thisRail.startTime) / (thisRail.endTime - thisRail.startTime));

    }
}
