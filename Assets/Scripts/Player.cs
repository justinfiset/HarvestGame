using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public WaterIndicator waterIndicator;
    public TileIndicator tileIndicator;
    public CrateUIPanel cratePanel;

    private void Awake()
    {
        inventory.player = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            CloseAll();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.ToggleVisibility();
        }
    }

    public void CloseAll()
    {
        if (cratePanel.IsOpen())
            cratePanel.HideCrateUI();
    }
}
