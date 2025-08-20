using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowObject : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private bool keepScale = true;
    private float baseSize = 5f;

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(m_target.position);

        if(keepScale)
        {
            float scaleFactor = Camera.main.orthographicSize / baseSize;
            transform.localScale = Vector3.one * (1f / scaleFactor);
        }
    }
}
