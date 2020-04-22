using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    // Start is called before the first frame update
    TMP_Text scoreText;
    GameController gmCtrl;
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        gmCtrl = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        string scoreString = "Score: " + gmCtrl.score.ToString() + "\nCollectible: " + gmCtrl.collectibleGot;
        scoreText.text = scoreString;
    }
}
