using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour, IPlayerInteractable
{
    [SerializeField] private int m_slotCount;
    private List<Slot> m_slots = new List<Slot>();

    public bool Interact(Player player)
    {
        if (player.cratePanel.IsAlreadyOpen()) return false;

        player.cratePanel.ShowCrateUI(this);
        return true;
    }
}
