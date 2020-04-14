using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Circle
{
    public Vector2 Center { get; set; }
    public float Radius { get; set; }

    public Circle(Vector2 center, float radius)
    {
        Center = center;
        Radius = radius;
    }

    public bool Contains(Circle circle)
    {
        Vector2 v = Center - circle.Center;
        float distance = v.sqrMagnitude;
        return distance <= ((Radius * Radius) + (circle.Radius * circle.Radius));
    }
}
