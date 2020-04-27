using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text perfectText;
    public TMP_Text goodText;
    public TMP_Text missText;
    public TMP_Text collectibleText;

    public void updateUI(int perfect, int good, int miss, int collectibleGot, int collectibleCount)
    {
        perfectText.text = perfect.ToString();
        goodText.text = good.ToString();
        missText.text = miss.ToString();
        collectibleText.text = collectibleGot.ToString() + " / " + collectibleCount.ToString();
    }
}
