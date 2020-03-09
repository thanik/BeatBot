using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    // Start is called before the first frame update
    TMP_Text scoreText;
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string scoreString = "Score: " + GameController.Instance.score.ToString() + "\nCollectible: " + GameController.Instance.collectibleCount;
        scoreText.text = scoreString;
    }
}
