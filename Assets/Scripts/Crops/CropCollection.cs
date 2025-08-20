using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CropCollection", menuName = "Game/CropCollection")]
public class CropCollection : ScriptableObject
{
    public List<CropData> m_crops;

    public CropData Get(CropType type)
    {
        foreach(CropData crop in m_crops)
            if (crop.Type == type) return crop;
        return null;
    }
}
