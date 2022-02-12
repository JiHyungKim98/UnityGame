using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera m_camera = null;
    [SerializeField] private Transform m_targetTrans = null;
    private Transform m_cachedTransform;

    [SerializeField]
    private Vector3 m_offset = new Vector3(0, 20, -20);
    private float m_smoothTime = 0.3f;
    private Vector3 m_velocity = Vector3.zero;
    public Camera cam { get { return m_camera; } }

    private void Awake()
    {
        if (m_camera == null) m_camera = GetComponent<Camera>();
        m_cachedTransform = transform;
    }

    private void LateUpdate()
    {
        if (m_targetTrans != null) SmoothFollow();
    }

    private void SmoothFollow()
    {
        Vector3 pos = m_targetTrans.position + m_offset;
        pos = new Vector3(pos.x, m_offset.y, pos.z);
        m_cachedTransform.position = Vector3.SmoothDamp(m_cachedTransform.position, pos, ref m_velocity, m_smoothTime);
    }

    public void SetCamera(Transform _leaderTransform)
    {
        m_targetTrans = _leaderTransform.transform;
    }
}
