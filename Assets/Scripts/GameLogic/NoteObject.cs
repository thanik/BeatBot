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
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<AudioSource>(out pressedSound);
        TryGetComponent<SpriteRenderer>(out spriteRenderer);
        thisRail = GetComponentInParent<Rail>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished && thisRail == GameController.Instance.currentRail)
        {
            if (noteType == NoteObjectTypeEnum.HIT)
            {
                float diffTime = GameController.Instance.gameTime - time;
                if (Input.GetButtonDown(buttonName) && Mathf.Abs(diffTime) <= 0.15f)
                {
                    pressed = true;
                    if (Mathf.Abs(diffTime) < 0.02f)
                    {
                        GameController.Instance.judgeText.text = "perfect";
                    }

                    else if (Mathf.Abs(diffTime) > 0.05f)
                    {
                        GameController.Instance.judgeText.text = "good:";
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

                    if (pressedSound)
                    {
                        pressedSound.Play();
                    }
                    if (spriteRenderer)
                    {
                        spriteRenderer.enabled = false;
                    }
                    finished = true;
                }

                if (!pressed && diffTime > 0.15f)
                {
                    // missed
                    Debug.Log("missed! " + diffTime);
                    GameController.Instance.judgeText.text = "miss";
                    finished = true;
                }
            }
            else if (noteType == NoteObjectTypeEnum.HOVER)
            {
                if (GameController.Instance.gameTime > time)
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
        else if (GameController.Instance.gameTime > time + 2f)
        {
            gameObject.SetActive(false);
        }
    }
}
