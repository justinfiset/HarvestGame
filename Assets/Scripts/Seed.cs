using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Tool
{
    private Crop m_crop;

    public Seed(Crop crop) : base(crop.GetName() + "seed", "Use this to plant " + crop.GetName() + ".", crop.cropData.SeedIcon)
    {
        m_crop = crop;
        useAction = () => { Plant(CropManager.Instance, Camera.main.ScreenToWorldPoint(Input.mousePosition)); };
    }

    void Plant(CropManager manager, Vector2 pos)
    {
        manager.Plant(pos, m_crop);
    }
}
