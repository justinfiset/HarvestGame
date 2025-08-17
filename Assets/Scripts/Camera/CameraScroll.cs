using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScroll : MonoBehaviour
{
    private Camera m_cam;

    [Header("Scroll Settings")]
    [SerializeField] private float m_scrollSensitivity = 1.0f;
    [SerializeField] private float m_minScaleValue = 1.0f;
    [SerializeField] private float m_maxScaleValue = 10.0f;

    void Start()
    {
        m_cam = GetComponent<Camera>();
    }

    void Update()
    {
        float scrollDelta = Input.mouseScrollDelta.y;

        Vector3 mousePosBefore = m_cam.ScreenToWorldPoint(Input.mousePosition);

        m_cam.orthographicSize = Mathf.Clamp(
            m_cam.orthographicSize - scrollDelta * m_scrollSensitivity,
            m_minScaleValue,
            m_maxScaleValue
        );

        m_cam.transform.position += mousePosBefore - m_cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
