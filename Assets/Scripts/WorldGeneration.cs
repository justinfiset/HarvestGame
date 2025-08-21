using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    [SerializeField] private Vector2 m_size = new Vector2(10, 10);

    [Header("Grass")]
    [SerializeField] GameObject m_grassContainer;
    [SerializeField] GameObject m_grassPrefab;
    [SerializeField] private float m_grassCount = 250;
    private HashSet<Vector2> m_usedPositions = new HashSet<Vector2>();

    private void Start()
    {
        int layerMask = ~LayerMask.GetMask("IgnoreRaycast2D");

        for (int i = 0; i < m_grassCount; i++)
        {
            Vector2 pos = GetUniqueRandomPosition(layerMask);
            GameObject grass = Instantiate(m_grassPrefab, m_grassContainer.transform);
            grass.transform.position = pos;
        }
    }

    private Vector2 GetUniqueRandomPosition(int layerMask)
    {
        for (int attempt = 0; attempt < 50; attempt++) // sécurité pour éviter boucle infinie
        {
            Vector2 pos = GetRandomPosition();
            pos.x = Mathf.Floor(pos.x) + 0.5f;
            pos.y = Mathf.Floor(pos.y) + 0.5f;

            if (!m_usedPositions.Contains(pos) && Physics2D.OverlapPoint(pos, layerMask) == null)
            {
                m_usedPositions.Add(pos);
                return pos;
            }
        }

        return Vector2.zero;
    }

    private Vector2 GetRandomPosition()
    {
        float x = Random.Range(-m_size.x / 2, m_size.x / 2);
        float y = Random.Range(-m_size.y / 2, m_size.y / 2);
        return (Vector2)transform.position + new Vector2(x, y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, m_size);
    }
}
