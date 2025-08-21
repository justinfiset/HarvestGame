using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateUIPanel : MonoBehaviour
{
    private Crate m_currentCrate;
    [SerializeField] private float m_animationDuration = 0.2f;
    private float m_openedPos;
    private float m_closedPos;

    public void Awake()
    {
        m_openedPos = Screen.width;
        m_closedPos = m_openedPos + 460.0f;
    }

    public bool IsOpen()
    {
        return m_currentCrate != null;
    }
        
    public void ShowCrateUI(Player player, Crate crate)
    {
        player.inventory.Show();
        m_currentCrate = crate;
        LeanTween.moveX(gameObject, m_openedPos, m_animationDuration);
    }

    public void HideCrateUI()
    {
        m_currentCrate = null;
        LeanTween.moveX(gameObject, m_closedPos, m_animationDuration);
    }
}
