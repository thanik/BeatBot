using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class BossFightController : MonoBehaviour
{
    public GameObject playerObject;
    public Camera mainCamera;
    public TMP_Text debugText;
    public TMP_Text judgeText;
    public AudioMixer mixer;
    public bool isPlaying;
    public int score;

    public float bpm;
    public float beatTimeOffset;

    public float gameTime;
    private AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        // gather all level objects

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {

            gameTime += (Time.deltaTime * music.pitch);
            if (Mathf.Abs(gameTime - music.time) > 0.02f)
            {
                music.time = gameTime;
            }
        }

        if (debugText)
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
            debugText.text = debugString;
        }

        if (Input.GetKeyDown(KeyCode.P) && !isPlaying)
        {

            StartCoroutine(startPlaying());

        }
    }

    IEnumerator startPlaying()
    {
        music.PlayScheduled(AudioSettings.dspTime + 1f);
        yield return new WaitForSeconds(1f);
        isPlaying = true;
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
