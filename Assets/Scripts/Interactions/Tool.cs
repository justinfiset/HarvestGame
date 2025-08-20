using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tool : Item
{
    public class ToolStrategy
    {
        Action<CropManager, Vector2> _useFn;

        public ToolStrategy(Action<CropManager, Vector2> useFn)
        {
            _useFn = useFn;
        }

        public void Use(CropManager manager, Vector2 position)
        {
            _useFn(manager, position);  
        }
    }

    private ToolStrategy strategy;

    public Tool(string name, string description, Sprite sprite, Action<CropManager, Vector2> useFn = null)
        : base(name, description, sprite, false, 1)
    {
        this.strategy = new ToolStrategy(useFn);

        SetAction((Player player) =>
        {
            Use(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        });
    }

    public Tool(string name, string description, string spriteId, Action<CropManager, Vector2> useFn = null)
        : this(name, description, TextureDatabase.Get(spriteId), useFn)
    { }

    public void Use(Vector2 position)
    {
        strategy?.Use(CropManager.Instance, position);
    }
}

public class Hoe : Tool
{
    public Hoe() : base("Hoe", "Use this to till the soil and prepare it for planting.", "Hoe", (manager, pos) =>
    {
        manager.Plow(pos);
    }) { }
}

public class WateringCan : Tool
{
    public float waterCapacity { get; private set; } = 100.0f;
    public float waterLevel { get; private set; }
    public float waterConsumption { get; private set; }  = 12.5f;

    public WateringCan() : base("Watering Can", "Use this to water your crops so they can grow.", "Watering Can") 
    {
        Refill();
        SetAction(WaterPlot);
    }

    private void WaterPlot(Player player)
    {
        if(waterLevel >= waterConsumption)
        {
            bool success = CropManager.Instance.WaterPlot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (success)
            {
                waterLevel -= waterConsumption;
                player.waterIndicator.UpdateIndicator(waterLevel, waterCapacity);
            }
        } 
        else
        {
            // TODO SHOW NOTIFICATION to tell the player what is happening -> not enought water
        }
    }

    public bool Refill()
    {
        bool alreadyFull = waterCapacity == waterLevel;
        waterLevel = waterCapacity;
        return !alreadyFull;
    }
}

public class Shovel : Tool
{
    public Shovel() : base("Shovel", "Use this to unplow your plot.", "Shovel", (manager, pos) =>
    {
        manager.Unplow(pos);
    })
    { }
}