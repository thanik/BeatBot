using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public int id;
    public int railId;
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
    GameController gmCtrl;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out pressedSound);
        thisRail = GetComponentInParent<Rail>();
        if (startTime == 0f && endTime == 0f)
        {
            Debug.LogWarning("Connector on rail " + transform.parent.gameObject.name + " start and end time haven't set yet.");
            gameObject.SetActive(false);
        }
        gmCtrl = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished && thisRail == gmCtrl.currentRail)
        {
            float diffTime = gmCtrl.gameTime - startTime;
            if (Input.GetButtonDown(buttonName) && Mathf.Abs(diffTime) <= 0.15f)
            {
                pressed = true;
                if (Mathf.Abs(diffTime) <= 0.035f)
                {
                    gmCtrl.judgeText.text = "perfect";
                    gmCtrl.score += pressedScore;
                    gmCtrl.playerObject.GetComponentInChildren<JudgementFont>().triggerJudge(0);
                }

                else if (Mathf.Abs(diffTime) > 0.035f)
                {
                    gmCtrl.judgeText.text = "good:";
                    gmCtrl.score += pressedScore / 2;
                    if (diffTime > 0)
                    {
                        Debug.Log("pressed! LATE:" + diffTime);
                        gmCtrl.judgeText.text += "late";
                        gmCtrl.playerObject.GetComponentInChildren<JudgementFont>().triggerJudge(2);
                    }
                    else
                    {
                        Debug.Log("pressed! EARLY:" + diffTime);
                        gmCtrl.judgeText.text += "early";
                        gmCtrl.playerObject.GetComponentInChildren<JudgementFont>().triggerJudge(1);
                    }
                }
                gmCtrl.connectorTrigger(this);
                finished = true;
            }
            
            if (!pressed && diffTime > 0.15f)
            {
                // missed
                //Debug.Log("missed! " + diffTime);
                gmCtrl.judgeText.text = "miss";
                gmCtrl.connectorTrigger(this);
                finished = true;
            }
        }
    }
}
