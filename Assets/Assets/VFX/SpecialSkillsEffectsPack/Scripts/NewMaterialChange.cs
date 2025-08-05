using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMaterialChange : MonoBehaviour
{
    public bool isParticleSystem;
    public Material m_inputMaterial;
    Material m_objectMaterial;
    MeshRenderer m_meshRenderer;
    ParticleSystemRenderer m_particleRenderer;
    public float m_timeToReduce;
    public float m_reduceFactor =0.0f;
    public float m_upFactor;

    void Awake()
    {
        if (isParticleSystem)
        {
            m_particleRenderer = gameObject.GetComponent<ParticleSystemRenderer>();
            m_particleRenderer.material = m_inputMaterial;
            m_objectMaterial = m_particleRenderer.material;
        }
        else
        {
            m_meshRenderer = gameObject.GetComponent<MeshRenderer>();
            m_meshRenderer.material = m_inputMaterial;
            m_objectMaterial = m_meshRenderer.material;
        }
    }
}
