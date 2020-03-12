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
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished && thisRail == GameController.Instance.currentRail)
        {
            float diffTime = GameController.Instance.gameTime - startTime;
            if (Input.GetButtonDown(buttonName) && Mathf.Abs(diffTime) <= 0.15f)
            {
                pressed = true;
                if (Mathf.Abs(diffTime) <= 0.035f)
                {
                    GameController.Instance.judgeText.text = "perfect";
                    GameController.Instance.score += pressedScore;
                }

                else if (Mathf.Abs(diffTime) > 0.035f)
                {
                    GameController.Instance.judgeText.text = "good:";
                    GameController.Instance.score += pressedScore / 2;
                    if (diffTime > 0)
                    {
                        Debug.Log("pressed! LATE:" + diffTime);
                        GameController.Instance.judgeText.text += "late";
                    }
                    else
                    {
                        Debug.Log("pressed! EARLY:" + diffTime);
                        GameController.Instance.judgeText.text += "early";
                    }
                }
                GameController.Instance.connectorTrigger(this);
                finished = true;
            }
            
            if (!pressed && diffTime > 0.15f)
            {
                // missed
                //Debug.Log("missed! " + diffTime);
                GameController.Instance.judgeText.text = "miss";
                GameController.Instance.connectorTrigger(this);
                finished = true;
            }
        }
    }
}
