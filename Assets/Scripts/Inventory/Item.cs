using UnityEngine;
using System;

public class Item
{
    public string itemName;
    public string description;
    public Sprite icon;
    public bool stackable = true;
    public int maxStack = 16;
    public Action<Player> useAction;

    public Item(string name, string desc, Sprite icon, bool stackable, int maxStack, Action<Player> useAction = null)
    {
        this.itemName = name;
        this.description = desc;
        this.icon = icon;
        this.stackable = stackable;
        this.maxStack = maxStack;
        this.useAction = useAction;
    }

    public void SetAction(Action<Player> useFn)
    {
        this.useAction = useFn;
    }
}
