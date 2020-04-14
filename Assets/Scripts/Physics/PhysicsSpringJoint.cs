using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSpringJoint : PhysicsJoint
{
    public float RestLength { get; set; } = 0.0f;
    public float SpringPower { get; set; } = 20.0f;

    public override void ApplyForce(float dt)
    {
        Vector2 force = SpringForce(BodyA.Position, BodyB.Position, RestLength, SpringPower);

        // modifier modifies the amount of force to use, if there is a static object then apply 100% force to the objects if they are not static then split the force 50/50
        float modifier = (BodyA.Type == BodyTypeEnumRef.eType.Static || BodyB.Type == BodyTypeEnumRef.eType.Static) ? 1.0f : 0.5f;

        BodyA.ApplyForce(-force * modifier, PhysicsBody.eForceMode.IMPULSE);
        BodyB.ApplyForce(force * modifier, PhysicsBody.eForceMode.IMPULSE);
    }

    public static Vector2 SpringForce(Vector2 anchor, Vector2 body, float restLength, float springPower)
    {
        Vector2 direction = body - anchor;
        float length = direction.magnitude;
        float x = length - restLength;
        return (-springPower * x) * direction.normalized;
    }
}
