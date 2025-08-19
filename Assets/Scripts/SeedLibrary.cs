using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "Game/SeedLibrary", order = 1)]
public class SeedLibrary : ScriptableObject
{
    [Serializable]
    public class SeedMeta
    {
        Crop crop;
        Sprite texture;
    }
}