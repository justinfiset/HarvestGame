using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Player m_player;

    private KeyCode m_useKey = KeyCode.Mouse0;

    private void Update()
    {
        if(Input.GetKeyDown(m_useKey))
        {
            int layerMask = ~LayerMask.GetMask("IgnoreRaycast2D");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100f, layerMask);

            bool interacted = false;
            if (hit)
            {
                IPlayerInteractable interaction = hit.transform.gameObject.GetComponent<IPlayerInteractable>();
                if (interaction != null)
                    interacted = interaction.Interact(m_player);
            }

            if(!interacted)
            {
                m_player.inventory.UseItem(m_player);
            }
        }
    }
}
