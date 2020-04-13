using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeIndicatorUI : MonoBehaviour
{
    public Image chargeFillImage;

    public void updateChargeBar(float startTime, float currentTime, float endHoldTime)
    {
        if (currentTime < endHoldTime)
        {
            chargeFillImage.fillAmount = (currentTime / endHoldTime) * 0.85f;
        }
        else
        {
            chargeFillImage.fillAmount = 1f;
        }
    }
}
