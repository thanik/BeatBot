using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public NoteObjectTypeEnum noteType;
    public float time;
    public string buttonName;
    public AudioSource pressedSound;
    public bool pressed;
    public bool finished;

    private Rail thisRail;
    private SpriteRenderer spriteRenderer;
    GameController gmCtrl;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<AudioSource>(out pressedSound);
        TryGetComponent<SpriteRenderer>(out spriteRenderer);
        thisRail = GetComponentInParent<Rail>();
        gmCtrl = FindObjectOfType<GameController>();
        gmCtrl.collectibleCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished && thisRail == FindObjectOfType<GameController>().currentRail)
        {
            if (noteType == NoteObjectTypeEnum.HIT)
            {
                float diffTime = gmCtrl.gameTime - time;
                if (Mathf.Abs(diffTime) <= 0.25f)
                {
                    gmCtrl.FButtonUI.gameObject.SetActive(true);
                }

                if (Input.GetButtonDown(buttonName) && Mathf.Abs(diffTime) <= 0.1f)
                {
                    pressed = true;
                    if (Mathf.Abs(diffTime) <= 0.03f)
                    {
                        gmCtrl.judgeText.text = "perfect";
                        gmCtrl.playerObject.GetComponentInChildren<JudgementFont>().triggerJudge(0);
                    }

                    else if (Mathf.Abs(diffTime) > 0.03f)
                    {
                        FindObjectOfType<GameController>().judgeText.text = "good:";
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

                    if (pressedSound)
                    {
                        pressedSound.Play();
                    }
                    if (spriteRenderer)
                    {
                        spriteRenderer.enabled = false;
                    }
                    finished = true;
                    gmCtrl.FButtonUI.gameObject.SetActive(false);
                    gmCtrl.collectibleGot++;
                }

                if (!pressed && diffTime > 0.1f)
                {
                    // missed
                    Debug.Log("missed! " + diffTime);
                    gmCtrl.judgeText.text = "miss";
                    gmCtrl.playerObject.GetComponentInChildren<JudgementFont>().triggerJudge(3);
                    gmCtrl.FButtonUI.gameObject.SetActive(false);
                    finished = true;
                }
            }
            else if (noteType == NoteObjectTypeEnum.HOVER)
            {
                if (gmCtrl.gameTime > time)
                {

                    if (spriteRenderer)
                    {
                        spriteRenderer.enabled = false;
                    }

                    if (pressedSound)
                    {
                        pressedSound.Play();
                    }

                    finished = true;
                }
            }
        }
        else if (gmCtrl.gameTime > time + 3f)
        {
            gameObject.SetActive(false);
        }
    }
}
