using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsWorld : MonoBehaviour
{
    [SerializeField] FloatRef m_gravity = null;
    [SerializeField] FloatRef m_fps = null;
    [SerializeField] BoolRef m_simulate = null;
    [HideInInspector] public List<PhysicsBody> bodies = new List<PhysicsBody>();
    [HideInInspector] public List<PhysicsJoint> joints = new List<PhysicsJoint>();
    public static Vector2 Gravity { get; set; } = new Vector2(0, -9.81f);

    public float FixedTimeStep { get => 1.0f / m_fps.value; }

    float timeAccumulator = 0.0f;
    void Update()
    {
        Gravity = new Vector2(0.0f, m_gravity.value);
        timeAccumulator += (m_simulate.value) ? Time.deltaTime : 0;

        joints.ForEach(joint => joint.ApplyForce(Time.deltaTime));
        while (timeAccumulator > FixedTimeStep)
        {
            bodies.ForEach(body => {
                body.Step();
                Integrator.SemiImplicitEuler(body, Time.deltaTime);
                body.Shape.color = Color.white;
            });

            //check collision
            for (int i = 0; i < bodies.Count; i++)
            {
                for (int j = i + 1; j < bodies.Count; j++)
                {
                    if (Collision.TestOverlap(bodies[i].Shape, bodies[i].Position, bodies[j].Shape, bodies[j].Position))
                    {
                        bodies[i].Shape.color = Color.red;
                        bodies[j].Shape.color = Color.red;
                    }
                }
            }

            timeAccumulator -= FixedTimeStep;
        }
        joints.ForEach(joint => joint.DebugDraw());
        bodies.ForEach(body => {
            body.Force = Vector2.zero;
            body.Accel = Vector2.zero;
        });
    }

    public static PhysicsBody BodyAtPosition(Vector2 position)
    {
        PhysicsBody body = null;

        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit.collider != null)
        {
            body = hit.collider.gameObject.GetComponent<PhysicsBody>();
        }

        return body;
    }
}
