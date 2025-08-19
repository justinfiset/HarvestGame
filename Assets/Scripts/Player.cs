using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Inventory m_inventory; // Only to set in the editro
    public Inventory inventory => m_inventory; // visible to every class
}
