using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBody : MonoBehaviour
{
    public enum eForceMode
    {
        ACCEL,
        VELOCITY,
        IMPULSE,
        FORCE
    }

    [SerializeField] Shape m_shape = null;

    public BodyTypeEnumRef.eType Type { get; set; } = BodyTypeEnumRef.eType.Dynamic;
    public Vector2 Position { get => transform.position; set => transform.position = value; }

    public Vector2 Force { get; set; }
    public Vector2 Accel { get; set; }
    public Vector2 Velocity { get; set; }
    public float Mass { get; set; } = 1.0f;
    public float GravityScale { get; set; } = 1.0f;

    public float Damping { get; set; } = 1.0f;

    public PhysicsWorld World { get; set; } = null;
    public Shape Shape { get => m_shape; set => m_shape = value; }

    public void ApplyForce(Vector2 force, eForceMode mode)
    {
        if (Type != BodyTypeEnumRef.eType.Dynamic) return;

        switch (mode)
        {
            case eForceMode.ACCEL:
                this.Accel = force;
                break;
            case eForceMode.VELOCITY:
                this.Velocity = force;
                break;
            case eForceMode.IMPULSE:
                this.Force += force;
                break;
            case eForceMode.FORCE:
                this.Force += force * World.FixedTimeStep;
                break;
        }
    }

    public void Step(float dt)
    {
        if (Type != BodyTypeEnumRef.eType.Dynamic) return;

        Accel += (PhysicsWorld.Gravity * GravityScale) + (Force / Mass);
    }

    public void Step()
    {
        Step(World.FixedTimeStep);
    }
}
