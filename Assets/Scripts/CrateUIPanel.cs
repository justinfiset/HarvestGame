using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateUIPanel : MonoBehaviour
{
    private Crate m_currentCrate;

    public bool IsAlreadyOpen()
    {
        return m_currentCrate != null;
    }

    public void ShowCrateUI(Crate crate)
    {
        m_currentCrate = crate;
    }

    public void HideCrateUI()
    {
        m_currentCrate = null;
    }
}
