using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class WaterSource : MonoBehaviour, IPlayerInteractable
{
    [SerializeField] private SoundCollection m_waterRefillCollection;
    private AudioSource m_audioSource;

    [SerializeField] private string m_name;
    [SerializeField] private string m_description;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public bool Interact(Player player)
    {
        Slot slot = player.inventory.GetCurrentSlot();

        if(typeof(WateringCan) == slot.item.GetType())
        {
            WateringCan can = (WateringCan)slot.item;
            if(can.Refill())
            {
                m_waterRefillCollection.PlayRandom(m_audioSource);
                player.waterIndicator.UpdateIndicator(can.waterLevel, can.waterCapacity);
            }
            return true;
        }

        return false;
    }

    public ItemTooltipData GetTooltipData()
    {
        return new ItemTooltipData(m_name, m_description);
    }
}
