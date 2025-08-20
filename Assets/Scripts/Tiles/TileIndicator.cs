using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileIndicator : MonoBehaviour
{
    private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Color m_regularColor;
    [SerializeField] private Color m_warningColor;

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 tilePos = CropManager.GetTilePosition(mousePos);
        transform.position = tilePos;
    }

    public void UpdateState(bool canInteract)
    {
        m_spriteRenderer.color = canInteract ? m_regularColor : m_warningColor;
    }
}
