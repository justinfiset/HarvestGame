using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FieldPlot : MonoBehaviour
{
    private SpriteRenderer m_spriteRenderer;

    private bool m_isWatered;
    private Crop m_crop;
    private int m_completionState;
    private static readonly int m_completionMax = 3;

    public void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        NotifyUpdate();
    }

    public void SetWaterStatus(bool watered)
    {
        m_isWatered = watered;

        /*TODO : REMOVE FOR DEBUG PURPOSES */
        m_spriteRenderer.color = new Color32(36, 25, 28, 255);

        NotifyUpdate();
    }

    public void NotifyUpdate()
    {
        // Impl NotifyUpdate();
    }
}
