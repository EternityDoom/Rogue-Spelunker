using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Integrator
{
    public static void ExplicitEuler(PhysicsBody body, float dt)
    {
        body.Position += body.Velocity * dt;
        body.Velocity += body.Accel * dt;
        body.Velocity *= 1.0f / (1.0f + body.Damping * dt);
    }

    public static void SemiImplicitEuler(PhysicsBody body, float dt)
    {
        body.Velocity += body.Accel * dt;
        body.Velocity *= 1.0f / (1.0f + body.Damping * dt);
        body.Position += body.Velocity * dt;
    }
}
