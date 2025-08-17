using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Slot : MonoBehaviour
{
    private static readonly string REGULAR_SLOT = "regular slot";
    private static readonly string SELECTED_SLOT = "selected slot";

    public Item item = null;
    public int quantity = 0;

    private Image m_backgroundImage;
    [SerializeField] private Image m_icon;

    private void Awake()
    {
        m_backgroundImage = GetComponent<Image>();
        UpdatePlaceholder();
    }

    public void SetSelected(bool newStatus)
    {
        string sprite = newStatus ? SELECTED_SLOT : REGULAR_SLOT;
        m_backgroundImage.sprite = TextureDatabase.Get(sprite);
    }

    public void SetItem(Item item, int quantity = 1)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public void UpdatePlaceholder()
    {
        m_icon.sprite = (item != null) ? item.icon : null;
        m_icon.color = (m_icon.sprite == null) ? Color.clear : Color.white;
    }
}
