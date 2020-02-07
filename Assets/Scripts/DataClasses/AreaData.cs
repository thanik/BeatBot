using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AreaData
{
    public int id;
    public Vector3 topCorner;
    public Vector3 bottomCorner;
    public float songPitch;
    public float songEffectPercentage;
    public int songEffectType;
}
