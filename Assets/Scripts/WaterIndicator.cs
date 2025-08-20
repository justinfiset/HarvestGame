using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterIndicator : MonoBehaviour
{
    [SerializeField] private Image m_indicatorImage;
    private float m_baseSize;
    [SerializeField] private float m_animationSpeed = 0.1f;

    private void Start()
    {
        m_baseSize = m_indicatorImage.rectTransform.sizeDelta.y;
    }

    public void UpdateIndicator(float current, float capacity)
    {
        float goalY = (current / capacity) * m_baseSize;
        Vector2 goalSize = new Vector2(m_indicatorImage.rectTransform.sizeDelta.x, goalY);
        LeanTween.size(m_indicatorImage.rectTransform, goalSize, m_animationSpeed);
    }
}
