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

        SetAction(() =>
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
    public WateringCan() : base("Watering Can", "Use this to water your crops so they can grow.", "Watering Can", (manager, pos) =>
    {
        manager.WaterPlot(pos);
    }) { }
}

public class Shovel : Tool
{
    public Shovel() : base("Shovel", "Use this to unplow your plot.", "Shovel", (manager, pos) =>
    {
        manager.Unplow(pos);
    })
    { }
}