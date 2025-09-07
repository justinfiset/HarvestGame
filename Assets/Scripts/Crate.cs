using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour, IPlayerInteractable
{
    [SerializeField] private int m_slotCount;
    private List<Slot> m_slots = new List<Slot>();

    public ItemTooltipData GetTooltipData()
    {
        return new ItemTooltipData("Crate", "Use this to store items.");
    }

    public bool Interact(Player player)
    {
        if (player.cratePanel.GetCrate() == this)
        {
            player.CloseAll();
        } 
        else
        {
            player.cratePanel.ShowCrateUI(player, this);
        }
        return true;
    }
}
