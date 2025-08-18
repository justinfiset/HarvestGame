using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FieldPlot : MonoBehaviour
{
    private SpriteRenderer m_spriteRenderer;

    private bool m_isWatered;
    private Crop m_crop;
    private int m_plowState = 1;
    private static readonly int m_maxPlowState = 3;

    public void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        NotifyUpdate();
    }

    /// <summary>
    /// Fonction to plow the ground, returns true if the parcel is completed
    /// </summary>
    /// <returns></returns>
    public bool Plow()
    {
        if (m_plowState == m_maxPlowState) return true;

        m_plowState += 1;
        NotifyUpdate();

        return false;
    }

    public void WaterPlot()
    {
        if(m_plowState == m_maxPlowState)
        {
            m_isWatered = true;
            NotifyUpdate();
        }
    }

    public void NotifyUpdate()
    {
        /* TODO REMOVE FOR DEBUG ONLY */
        m_spriteRenderer.gameObject.transform.localScale = Vector3.one * m_plowState / m_maxPlowState;

        /*TODO : REMOVE FOR DEBUG PURPOSES */
        m_spriteRenderer.color = m_isWatered ? new Color32(36, 25, 28, 255) : new Color32(164, 118, 74, 255);

        // Impl NotifyUpdate();
    }
}