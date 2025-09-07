using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public interface IItemTooltip
{
    public ItemTooltipData GetTooltipData();
}

public class ItemTooltipData
{
    public string Name;
    public string Description;

    public ItemTooltipData(string name, string description)
    {
        this.Name = name;
        this.Description = description;
    }
}

[RequireComponent(typeof(CanvasGroup))]
public class ItemTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_nameText;
    [SerializeField] private TextMeshProUGUI m_descriptionText;

    private CanvasGroup m_canvasGroup;

    public void Start()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
    }

    public void DisplayTooltip(ItemTooltipData data)
    {
        if(data != null)
        {
            m_canvasGroup.alpha = 1;
            m_nameText.text = data.Name;
            m_descriptionText.text = data.Description;
        }
    }

    internal void DisplayTooltip(object p)
    {
        throw new NotImplementedException();
    }

    public void HideTooltip()
    {
        m_canvasGroup.alpha = 0;
    }
}
