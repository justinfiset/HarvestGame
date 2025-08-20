using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop
{
    public CropData cropData;

    public Crop(CropData data)
    {
        cropData = data;
    }

    public string GetName() 
    {
        return cropData.name;
    }
}
