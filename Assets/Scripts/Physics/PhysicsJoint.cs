using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicsJoint
{
    public PhysicsBody BodyA { get; set; } = null;
    public PhysicsBody BodyB { get; set; } = null;

    public abstract void ApplyForce(float dt);
    public void DebugDraw()
    {
        Debug.DrawLine(BodyA.Position, BodyB.Position);
    }
}
