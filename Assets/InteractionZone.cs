using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class InteractionZone : MonoBehaviour
{
    [HideInInspector] public Vector2 min; // lower left corner
    [HideInInspector] public Vector2 max; // upper right corner
    private BoxCollider2D col;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();

        Vector3 center = col.bounds.center;
        Vector3 extends = col.bounds.extents;
        min = center - extends;
        max = center + extends;
    }

    public bool Contains(Vector2 point)
    {
        return point.x >= min.x && point.x <= max.x &&
               point.y >= min.y && point.y <= max.y;
    }
}