using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScroll : MonoBehaviour
{
    private Camera m_cam;

    [Header("Scroll Settings")]
    float m_scrollSensitivity = 1.0f;
    float m_minScaleValue = 1.0f;
    float m_maxScaleValue = 10.0f;

    void Start()
    {
        m_cam = GetComponent<Camera>();
    }

    void Update()
    {
        float dt = Input.mouseScrollDelta.y;
        m_cam.orthographicSize += dt * m_scrollSensitivity;

        Vector3 offset = m_cam.ScreenToWorldPoint(Input.mousePosition) - m_cam.transform.position;
        m_cam.transform.position += offset * m_scrollSensitivity * dt;
    }
}
