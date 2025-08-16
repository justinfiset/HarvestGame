using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraDrag : MonoBehaviour
{
    private Camera m_cam;
    private KeyCode m_mouseButton = KeyCode.Mouse1;

    private Vector3 m_dragOrigin;

    private void Start()
    {
        m_cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(m_mouseButton))
        {
            m_dragOrigin = m_cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetKey(m_mouseButton))
        {
            Vector3 diff = m_dragOrigin - m_cam.ScreenToWorldPoint(Input.mousePosition);
            m_cam.transform.position += diff;
        }
    }
}
