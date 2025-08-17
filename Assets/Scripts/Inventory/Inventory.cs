using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private static int s_slotbarCount = 4;
    private static int s_slotCount = 20;

    [Header("UI")]
    [SerializeField] private GameObject m_slotTemplate;
    [SerializeField] private GameObject m_slotbarContainer;
    [SerializeField] private GameObject m_inventoryContainer;
    [SerializeField] private Image m_cursor;

    private int m_currentSlotbarIndex = 0;
    private bool m_isShowing = false;

    private List<Slot> m_slots = new List<Slot>(s_slotCount);

    private void Start()
    {
        for(int i = 0; i < s_slotbarCount; i++)
            m_slots.Add(Instantiate(m_slotTemplate, m_slotbarContainer.transform).GetComponent<Slot>());

        for (int i = 4; i < s_slotCount; i++)
            m_slots.Add(Instantiate(m_slotTemplate, m_inventoryContainer.transform).GetComponent<Slot>());

        Show(false);
        SetCurrentSelectItem(0);

        AddItem(new Hoe());
        AddItem(new WateringCan());
    }

    void Update()
    {
        for (int i = 0; i < s_slotbarCount && i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SetCurrentSelectItem(i);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Show(!m_isShowing);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Item it = m_slots[m_currentSlotbarIndex].item;
            it?.useAction();
        }
    }

    void SetCurrentSelectItem(int index)
    {
        m_slots[m_currentSlotbarIndex].SetSelected(false); // Unselected old slot

        m_currentSlotbarIndex = index;
        m_slots[m_currentSlotbarIndex].SetSelected(true); // Select new slot

        // Update the cursor
        m_cursor.sprite = (m_slots[index].item != null) ? m_slots[index].item.icon : null;
        m_cursor.color = (m_cursor.sprite == null) ? Color.clear : Color.white;
    }
    
    void Show(bool showStatus)
    {
        m_isShowing = showStatus;
        m_inventoryContainer.SetActive(showStatus);
    }

    public bool AddItem(Item item, int quantity = 1)
    {
        if(item.stackable)
        {
            Slot slot = m_slots.Find(s =>
                s.item != null &&
                s.item.itemName == item.itemName &&
                s.quantity < item.maxStack
            );

            if (slot != null)
            {
                int addAmount = Mathf.Min(quantity, item.maxStack - slot.quantity);
                slot.quantity += addAmount;
                quantity -= addAmount;
                slot.UpdatePlaceholder();
            }
        }

        for (int i = 0; i < m_slots.Count; i++)
        {
            if (m_slots[i].item == null)
            {
                int stackAmount = item.stackable ? Mathf.Min(quantity, item.maxStack) : 1;
                m_slots[i].SetItem(item, stackAmount);
                quantity -= stackAmount;

                m_slots[i].UpdatePlaceholder();

                if (quantity == 0) break;
            }
        }

        if(quantity != 0)
        {
            // TODO SHOW INVENTORY FULL ERROR / WARNING MESSGE VIA PANEL
        }

        // Reselect just to update the icon
        SetCurrentSelectItem(m_currentSlotbarIndex);

        return quantity == 0;
    }
}
