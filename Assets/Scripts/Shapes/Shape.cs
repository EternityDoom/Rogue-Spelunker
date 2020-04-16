using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shape : MonoBehaviour
{
    public enum eType
    {
        CIRCLE,
        SQUARE
    }

    private float m_radius;

    public float radius { get { return m_radius * 0.125f; } set { m_radius = value; transform.localScale = Vector2.one * value; } }

    public abstract eType type { get; }
    public float density { get; set; } = 1.0f;
    public Color color { 
        get 
        {
            return GetComponent<Renderer>().material.color;
        }
        set 
        {
            GetComponent<Renderer>().material.color = value;
        } 
    }

    public abstract float ComputeMass(float density);
}