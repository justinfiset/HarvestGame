using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private static int s_slotbarCount = 4;
    private static int s_slotCount = 20;

    public Player player;

    [Header("Debug")]
    [SerializeField] private CropCollection m_cropCollection;

    [Header("UI")]
    [SerializeField] private GameObject m_slotTemplate;
    [SerializeField] private GameObject m_slotbarContainer;
    [SerializeField] private GameObject m_inventoryContainer;
    [SerializeField] private Image m_inventoryBackground;
    [SerializeField] private InteractionZone m_usageZone;

    [Header("Cursor")]
    // TODO : MOVE TO A CURSOR DEDICATED SCRIPT
    [SerializeField] private Image m_cursor;
    [SerializeField] private Image m_cursorError;

    private int m_currentSlotbarIndex = 0;
    private bool m_isShowing = false;

    private List<Slot> m_slots = new List<Slot>(s_slotCount);

    private void Start()
    {
        for(int i = 0; i < s_slotbarCount; i++)
        {
            Slot slot = Instantiate(m_slotTemplate, m_slotbarContainer.transform).GetComponent<Slot>();

            int index = i;
            slot.onClickAction = () => {
                SetCurrentSelectItem(index); 
            };

            m_slots.Add(slot);
            slot.SetVisibility(true, i * 0.1f);
        }

        for (int i = 4; i < s_slotCount; i++)
            m_slots.Add(Instantiate(m_slotTemplate, m_inventoryContainer.transform).GetComponent<Slot>());

        Show(false);
        SetCurrentSelectItem(0);

        AddItem(new Hoe());
        AddItem(new WateringCan());
        AddItem(new Shovel());
        AddItem(m_cropCollection.Get(CropType.Carrot).GetSeed());
    }

    public Slot GetCurrentSlot()
    {
        return m_slots[m_currentSlotbarIndex];
    }

    public void UseItem(Player player)
    {
        if (m_usageZone.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            Item it = m_slots[m_currentSlotbarIndex].item;
            it?.useAction(player);
        }
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

        if (m_usageZone.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            m_cursorError.color = Color.clear; // Hide the warning
            player.tileIndicator.UpdateState(true);
        } else
        {
            m_cursorError.color = Color.white;
            player.tileIndicator.UpdateState(false);
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
    
    public void ToggleVisibility()
    {
        Show(!m_isShowing);
    }

    public void Show(bool showStatus)
    {
        if (showStatus) Show();
        else Hide();
    }

    public void Show()
    {
        if (m_isShowing) return;

        m_isShowing = true;

        float newX = 570;
        LeanTween.moveLocalX(m_inventoryBackground.gameObject, newX, 0.1f).setOnComplete(() => {
            m_inventoryContainer.SetActive(true);

            for (int i = s_slotbarCount; i < s_slotCount; i++)
            {
                m_slots[i].SetVisibility(m_isShowing, 0.02f * i);
            }
        });
    }

    public bool IsOpen()
    {
        return m_isShowing;
    }

    public void Hide()
    {
        if (!m_isShowing) return;

        m_isShowing = false;

        float newX = 130;
        LeanTween.moveLocalX(m_inventoryBackground.gameObject, newX, 0.1f);
        m_inventoryContainer.SetActive(false);

        for (int i = s_slotCount - 1; i >= s_slotbarCount; i--)
        {
            m_slots[i].SetVisibility(m_isShowing, 0.02f * i);
        }
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

        if(quantity > 0)
        {
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
