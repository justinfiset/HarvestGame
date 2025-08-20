using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CropType
{
    Carrot
}

[CreateAssetMenu(fileName = "NewCrop", menuName = "Game/Crop")]
public class CropData : ScriptableObject
{
    public CropType Type;   
    public string CropName;
    public string CropDescription;
    public Sprite Icon;
    public Sprite SeedIcon;
    public Sprite[] GrowthStages;
    public float TimePerStage;
    public Item Yield;
    public int MinYield;
    public int MaxYield;

    public Crop GetCrop()
    {
        return new Crop(this);
    }

    public Item GetYieldItem()
    {
        return new Item(CropName, CropDescription, Icon, true, 64);
    }

    public Seed GetSeed()
    {
        return new Seed(GetCrop());
    }
}
