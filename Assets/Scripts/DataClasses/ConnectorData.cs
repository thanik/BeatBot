using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ConnectorData
{
    public int id;
    public float time;
    public string buttonName;

    public NoteObjectTypeEnum pressedDestinationObjectType;
    public int pressedDestinationObjectID;

    public NoteObjectTypeEnum unpressedDestinationObjectType;
    public int unpressedDestinationObjectID;
}
