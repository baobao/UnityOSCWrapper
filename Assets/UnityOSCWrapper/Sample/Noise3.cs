using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise3 : MonoBehaviour 
{
    [SerializeField]
    Shader m_shader;
    Material m_mat;
    [Range(0, 1)]
    public float horizonValue;
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if(m_shader == null) return;
        if (m_mat == null)
        {
            m_mat = new Material(m_shader);
            m_mat.hideFlags = HideFlags.DontSave;
        }
        m_mat.SetInt("_Seed", Time.frameCount);
        m_mat.SetFloat ("_HorizonValue", Mathf.Clamp01 (horizonValue) * 0.17f);
        Graphics.Blit(src, dest, m_mat);
    }
}