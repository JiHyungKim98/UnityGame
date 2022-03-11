using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera m_camera = null;
    [SerializeField] private Transform m_targetTrans = null;
    private Transform m_cachedTransform;
    Renderer ObstacleRenderer;
    Material Mat;
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
    private void Update()
    {
        float Distance = Vector3.Distance(transform.position, m_targetTrans.position);
        Vector3 Direction = (m_targetTrans.transform.position - transform.position).normalized;
        RaycastHit hit;
        if(Physics.Raycast(transform.position,Direction,out hit, Distance))
        {
            ObstacleRenderer = hit.transform.gameObject.GetComponentInChildren<Renderer>();
            Mat = ObstacleRenderer.material;
            if (ObstacleRenderer != null)
            {
                Mat.SetFloat("_Mode", 3);
                Mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                Mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                Mat.SetInt("_ZWrite", 0);
                Mat.DisableKeyword("_ALPHATEST_ON");
                Mat.DisableKeyword("_ALPHABLEND_ON");
                Mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                Mat.renderQueue = 3000;
                Color matColor = Mat.color;
                matColor.a = 0.2f;
                Mat.color = matColor;

            }
            
        }
        else
        {
            if (ObstacleRenderer != null)
            {
                Mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                Mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                Mat.SetInt("_ZWrite", 1);
                Mat.DisableKeyword("_ALPHATEST_ON");
                Mat.DisableKeyword("_ALPHABLEND_ON");
                Mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                Mat.renderQueue = -1;
            }
            
        }
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
