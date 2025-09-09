using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CanvasGroup))]
public class Slot : MonoBehaviour, IPointerClickHandler, IPlayerInteractable
{
    private static readonly string REGULAR_SLOT = "regular slot";
    private static readonly string SELECTED_SLOT = "selected slot";

    public Action onClickAction;
    public Item item = null;
    public int quantity = 0;

    private Image m_backgroundImage;
    private CanvasGroup m_canvasGroup;
    [SerializeField] private Image m_icon;
    [SerializeField] private TMPro.TextMeshProUGUI m_quantityText;

    private void Awake()
    {
        m_backgroundImage = GetComponent<Image>();
        m_canvasGroup = GetComponent<CanvasGroup>();
        m_canvasGroup.alpha = 0.0f;
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

        m_quantityText.text = (quantity > 1) ? quantity.ToString() : "";
    }

    public void SetVisibility(bool visibility, float animationDuration)
    {
        LeanTween.alphaCanvas(m_canvasGroup, visibility ? 1.0f : 0.0f, animationDuration);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(onClickAction != null) onClickAction();
    }

    public bool Interact(Player player)
    {
        // TODO: IMPL SLOT INTERACTION
        throw new NotImplementedException();
    }

    public ItemTooltipData GetTooltipData()
    {
        return new ItemTooltipData(item.itemName, item.description);
    }
}
