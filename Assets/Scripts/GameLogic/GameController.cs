using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using JPBotelho;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public GameObject playerObject;
    public Camera mainCamera;
    public ChargeIndicatorUI chargeIndicator;
    public TMP_Text debugText;
    public TMP_Text judgeText;
    public AudioMixer mixer;
    public Canvas pauseMenu;
    public bool isPlaying;
    public bool isOnConnector;
    public bool isOnRail;
    public int score;
    public int collectibleGot;
    public int collectibleCount;
    public int combo;

    public List<AreaData> areas;
    public float timeToLockCamera;
    public float bpm;
    public float beatTimeOffset;

    public float gameTime;
    public Connector currentConnector;
    public Rail currentRail;

    private CatmullRom spline;
    private int currentArea;
    private AudioSource music;
    private Vector3 cameraOffset = Vector3.zero;

    private bool fallTrigger = false;
    private bool stillInWater = false;
    private float holdTime = 0f;
    private bool releaseInTime = false;
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        currentRail = FindObjectOfType<LevelSettings>().startRail;
        // gather all level objects
        StartCoroutine(startPlaying());
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            if (isOnConnector)
            {
                if (currentConnector.holdUntilTime > 0)
                {
                    if (Input.GetButton(currentConnector.buttonName))
                    {
                        chargeIndicator.gameObject.SetActive(true);
                        holdTime += Time.deltaTime;
                    }
                    chargeIndicator.updateChargeBar(0, holdTime, currentConnector.holdUntilTime - currentConnector.startTime);
                    float currentLerpTime = (gameTime - currentRail.startTime) / (currentRail.endTime - currentRail.startTime);
                    playerObject.transform.position = Vector3.Lerp(currentRail.transform.position, currentRail.endPosition, currentLerpTime);

                    if (Input.GetButtonUp(currentConnector.buttonName))
                    {
                        if(Mathf.Abs(holdTime - (currentConnector.holdUntilTime - currentConnector.startTime)) < 0.2f)
                        {
                            releaseInTime = true;
                        }
                        calculateMovementCurve(currentConnector, currentConnector.pressed && releaseInTime);
                    }

                    if ((!releaseInTime && currentConnector.pressed && Input.GetButtonUp(currentConnector.buttonName)) || (releaseInTime && currentConnector.pressed) || (gameTime - currentConnector.startTime > 0.15f && !currentConnector.pressed))
                    {
                        if (currentConnector.pressed && releaseInTime && currentConnector.pressedAction == ConnectorActionEnum.JUMP_TO_RAIL)
                        {
                            Debug.Log("releaseInTime");
                            currentLerpTime = (gameTime - currentConnector.holdUntilTime) / (currentConnector.endTime - currentConnector.holdUntilTime);
                            //playerObject.transform.position = Vector3.Lerp(currentConnector.transform.position, currentConnector.pressedToRail.transform.position, currentLerpTime);
                            playerObject.transform.position = LerpOverNumber(spline.GetPoints(), currentLerpTime);
                            playerObject.GetComponent<Animator>().SetBool("isJumping", true);
                            if (gameTime > currentConnector.endTime)
                            {
                                // finish connecting to other rail
                                currentRail = currentConnector.pressedToRail;
                                isOnConnector = false;
                                isOnRail = true;
                                releaseInTime = false;

                            }
                        }
                        else if (currentConnector.unpressedAction == ConnectorActionEnum.JUMP_TO_RAIL)
                        {
                            Debug.Log("release missed");
                            float endTime = currentConnector.unpressedEndTime > 0 ? currentConnector.unpressedEndTime : currentConnector.endTime;
                            currentLerpTime = (gameTime - currentConnector.startTime) / (endTime - currentConnector.startTime);
                            //playerObject.transform.position = Vector3.Lerp(currentConnector.transform.position, Vector3.Lerp(currentConnector.unpressedToRail.transform.position, currentConnector.unpressedToRail.endPosition, (endTime - currentConnector.unpressedToRail.startTime) / (currentConnector.unpressedToRail.endTime - currentConnector.unpressedToRail.startTime)), currentLerpTime);
                            playerObject.transform.position = LerpOverNumber(spline.GetPoints(), currentLerpTime);
                            if (gameTime > endTime)
                            {
                                // finish connecting to other rail
                                currentRail = currentConnector.unpressedToRail;
                                isOnConnector = false;
                                isOnRail = true;
                            }
                        }
                        else
                        {
                            Debug.Log("release missed");
                            isOnConnector = false;
                            isOnRail = true;
                        }
                        chargeIndicator.gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (currentConnector.pressed && currentConnector.pressedAction == ConnectorActionEnum.JUMP_TO_RAIL)
                    {
                        float currentLerpTime = (gameTime - currentConnector.startTime) / (currentConnector.endTime - currentConnector.startTime);
                        //playerObject.transform.position = Vector3.Lerp(currentConnector.transform.position, currentConnector.pressedToRail.transform.position, currentLerpTime);
                        playerObject.transform.position = LerpOverNumber(spline.GetPoints(), currentLerpTime);
                        playerObject.GetComponent<Animator>().SetBool("isJumping", true);
                        if (gameTime > currentConnector.endTime)
                        {
                            // finish connecting to other rail
                            currentRail = currentConnector.pressedToRail;
                            isOnConnector = false;
                            isOnRail = true;
                        }
                    }
                    else if (currentConnector.unpressedAction == ConnectorActionEnum.JUMP_TO_RAIL)
                    {
                        float endTime = currentConnector.unpressedEndTime > 0 ? currentConnector.unpressedEndTime : currentConnector.endTime;
                        float currentLerpTime = (gameTime - currentConnector.startTime) / (endTime - currentConnector.startTime);
                        //playerObject.transform.position = Vector3.Lerp(currentConnector.transform.position, Vector3.Lerp(currentConnector.unpressedToRail.transform.position, currentConnector.unpressedToRail.endPosition, (endTime - currentConnector.unpressedToRail.startTime) / (currentConnector.unpressedToRail.endTime - currentConnector.unpressedToRail.startTime)), currentLerpTime);
                        playerObject.transform.position = LerpOverNumber(spline.GetPoints(), currentLerpTime);
                        if (gameTime > endTime)
                        {
                            // finish connecting to other rail
                            currentRail = currentConnector.unpressedToRail;
                            isOnConnector = false;
                            isOnRail = true;
                        }
                    }
                    else if (currentConnector.finished && currentConnector.unpressedAction == ConnectorActionEnum.NOTHING)
                    {
                        isOnConnector = false;
                        isOnRail = true;
                    }
                }
                playerObject.GetComponent<Animator>().SetBool("isNearConnector", false);
            }
            else if (isOnRail)
            {
                //playerObject.transform.Translate(Vector3.right * Time.deltaTime * rails[currentRail].movingSpeed);
                playerObject.GetComponent<Animator>().SetBool("isJumping", false);
                float currentLerpTime = (gameTime - currentRail.startTime) / (currentRail.endTime - currentRail.startTime);
                playerObject.transform.position = Vector3.Lerp(currentRail.transform.position, currentRail.endPosition, currentLerpTime);
            }

            if (gameTime > timeToLockCamera)
            {
                if (cameraOffset == Vector3.zero)
                {
                    cameraOffset = mainCamera.transform.position - playerObject.transform.position;
                }
                mainCamera.transform.position = playerObject.transform.position + cameraOffset;
            }

            // find area
            //foreach(AreaData areaData in areas)
            //{
            //    if (areaData.topCorner)
            //}
            if (playerObject.transform.position.y < -2 && !stillInWater)
            {
                fallTrigger = true;
                mixer.SetFloat("lowPassFreq", 4000);
            }
            else if (playerObject.transform.position.y > -2)
            {
                stillInWater = false;
                mixer.SetFloat("lowPassFreq", 20000);
                music.pitch = 1f;
            }

            if(fallTrigger)
            {
                stillInWater = true;
                fallTrigger = false;
                music.pitch = 0.5f;
                music.DOPitch(1f, 4f);
            }

            gameTime += (Time.deltaTime * music.pitch);
            if (Mathf.Abs(gameTime - music.time) > 0.02f)
            {
                music.time = gameTime;
            }
        }

        if(debugText)
        {
            string debugString = "gameTime: ";
            debugString += gameTime.ToString("0.0000");
            debugString += " songTime: ";
            if (music)
            {
                debugString += music.time.ToString("0.0000") + " @ " + music.pitch.ToString("0.00");
                float timeDiff = gameTime - music.time;
                float absTimeDiff = Mathf.Abs(timeDiff);
                debugString += "\ntimeDiff: ";
                debugString += timeDiff.ToString("0.0000");
                debugString += "\n0.02: ";
                debugString += (absTimeDiff >= 0.02).ToString();
                debugString += "\n0.01: ";
                debugString += (absTimeDiff >= 0.01).ToString();
                debugString += "\n0.005: ";
                debugString += (absTimeDiff >= 0.005).ToString();
            }
            debugString += "\nonRail: " + isOnRail.ToString() + " onConnector: " + isOnConnector.ToString();
            debugText.text = debugString;
        }

        //if (Input.GetKeyDown(KeyCode.P) && !isPlaying)
        //{
            
        //    StartCoroutine(startPlaying());
            
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePause();
        }
    }

    public void togglePause()
    {
        if (isPlaying)
        {
            isPlaying = false;
            pauseMenu.gameObject.SetActive(true);
            pauseMenu.GetComponent<Page>().currentButtonIndex = 0;
            pauseMenu.GetComponent<Page>().currentButton = pauseMenu.GetComponent<Page>().buttons[pauseMenu.GetComponent<Page>().currentButtonIndex];
            pauseMenu.GetComponent<AudioSource>().Play();
            music.Pause();
        }
        else
        {
            isPlaying = true;
            pauseMenu.gameObject.SetActive(false);
            music.Play();
        }
    }
    
    public void connectorTrigger(Connector triggeredFrom)
    {
        isOnConnector = true;
        isOnRail = false;
        releaseInTime = false;
        holdTime = 0f;
        currentConnector = triggeredFrom;
        if (triggeredFrom.holdUntilTime <= 0)
        {
            calculateMovementCurve(triggeredFrom, triggeredFrom.pressed);
        }
    }

    public void calculateMovementCurve(Connector triggeredFrom, bool pressed)
    {
        // calculate curve
        triggeredFrom.positionCurve.Clear();
        if (pressed && triggeredFrom.pressedAction == ConnectorActionEnum.JUMP_TO_RAIL)
        {
            triggeredFrom.positionCurve.Add(playerObject.transform.position);
            Rail destination = triggeredFrom.pressedToRail;
            Vector3 destinationPos = Vector3.Lerp(destination.transform.position, destination.endPosition, (triggeredFrom.endTime - destination.startTime) / (destination.endTime - destination.startTime));
            if (triggeredFrom.additionalPressedPositionCurve.Count > 0)
            {
                foreach (Vector3 additionalPos in triggeredFrom.additionalPressedPositionCurve)
                {
                    triggeredFrom.positionCurve.Add(additionalPos);
                }
            }
            else
            {
                if (destinationPos.y >= playerObject.transform.position.y)
                {
                    triggeredFrom.positionCurve.Add(new Vector3(destinationPos.x - ((destinationPos.x - playerObject.transform.position.x) / 2.25f), destinationPos.y + 0.5f));
                }
                else
                {
                    triggeredFrom.positionCurve.Add(new Vector3(destinationPos.x - ((destinationPos.x - playerObject.transform.position.x) / 3), playerObject.transform.position.y + 0.25f));
                }
            }
            triggeredFrom.positionCurve.Add(destinationPos);
            spline = new CatmullRom(triggeredFrom.positionCurve.ToArray(), 16, false);

        }
        else if (!pressed && triggeredFrom.unpressedAction == ConnectorActionEnum.JUMP_TO_RAIL)
        {
            triggeredFrom.positionCurve.Add(playerObject.transform.position);
            Rail destination = triggeredFrom.unpressedToRail;
            float endTime = triggeredFrom.unpressedEndTime > 0 ? triggeredFrom.unpressedEndTime : triggeredFrom.endTime;
            Vector3 destinationPos = Vector3.Lerp(destination.transform.position, destination.endPosition, (endTime - destination.startTime) / (destination.endTime - destination.startTime));
            if (triggeredFrom.additionalUnpressedPositionCurve.Count > 0)
            {
                foreach (Vector3 additionalPos in triggeredFrom.additionalUnpressedPositionCurve)
                {
                    triggeredFrom.positionCurve.Add(additionalPos);
                }
            }
            else
            {
                triggeredFrom.positionCurve.Add(new Vector3(destinationPos.x - ((destinationPos.x - playerObject.transform.position.x) / 3), playerObject.transform.position.y - ((playerObject.transform.position.y - destinationPos.y) / 6)));
            }
            triggeredFrom.positionCurve.Add(destinationPos);
            spline = new CatmullRom(triggeredFrom.positionCurve.ToArray(), 16, false);

        }
    }

    IEnumerator startPlaying()
    {
        music.PlayScheduled(AudioSettings.dspTime);
        music.volume = 0f;
        yield return new WaitForSeconds(1f);
        music.volume = 1f;
        isPlaying = true;
    }

    public void resetLevel()
    {
        mixer.SetFloat("lowPassFreq", 20000);
        //music.pitch = 1f;
        //gameTime = 0f;
        //cameraOffset = Vector3.zero;

        //fallTrigger = false;
        //stillInWater = false;
        // currentRail = startRail;
    }

    public Vector3 LerpOverNumber(Vector3[] vectors, float time)
    {
        time = Mathf.Clamp01(time);
        if (vectors == null || vectors.Length == 0)
        {
            throw (new System.Exception("Vectors input must have at least one value"));
        }
        if (vectors.Length == 1)
        {
            return vectors[0];
        }

        if (time == 0)
        {
            return vectors[0];
        }

        if (time == 1)
        {
            return vectors[vectors.Length - 1];
        }

        float t = time * (vectors.Length - 1);
        int p = (int)Mathf.Floor(t);
        t -= p;
        return Vector3.Lerp(vectors[p], vectors[p + 1], t);
    }
}
