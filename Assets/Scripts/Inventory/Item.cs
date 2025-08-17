using UnityEngine;
using System;

public class Item
{
    public string itemName;
    public string description;
    public Sprite icon;
    public bool stackable = true;
    public int maxStack = 16;
    public Action useAction;

    public Item(string name, string desc, Sprite icon, bool stackable, int maxStack, Action useAction = null)
    {
        this.itemName = name;
        this.description = desc;
        this.icon = icon;
        this.stackable = stackable;
        this.maxStack = maxStack;
        this.useAction = useAction;
    }

    public void SetAction(Action useFn)
    {
        this.useAction = useFn;
    }
}
