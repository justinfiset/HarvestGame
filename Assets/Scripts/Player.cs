using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public WaterIndicator waterIndicator;
    public TileIndicator tileIndicator;

    private void Awake()
    {
        inventory.player = this;
    }
}
