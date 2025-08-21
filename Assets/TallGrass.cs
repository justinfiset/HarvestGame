using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TallGrass : MonoBehaviour, IPlayerInteractable
{
    [SerializeField] private SoundCollection m_clipCollection;
    [SerializeField] private SpriteCollection m_sprites;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = m_sprites.GetRandomSprite();
    }

    public bool Interact(Player player)
    {
        AudioSource.PlayClipAtPoint(m_clipCollection.GetRandom(), transform.position);
        Destroy(gameObject);
        return true;
    }
}
