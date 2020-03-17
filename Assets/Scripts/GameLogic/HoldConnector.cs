using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldConnector : MonoBehaviour
{
    public string buttonName;

    public float startTime;
    public float endTime;

    public ConnectorActionEnum pressedAction;
    public Rail pressedToRail;
    public List<Vector3> additionalPressedPositionCurve;
    public int pressedScore;

    public ConnectorActionEnum unpressedAction;
    public Rail unpressedToRail;
    public List<Vector3> additionalUnpressedPositionCurve;
    public float unpressedEndTime;
    public int unpressedScore;

    public List<Vector3> positionCurve;
    public AudioSource pressedSound;
    public bool pressed;
    public bool finished;

    private Rail thisRail;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
