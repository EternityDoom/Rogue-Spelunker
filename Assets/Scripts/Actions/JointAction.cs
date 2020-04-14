using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointAction : Action
{
	[SerializeField] LineRenderer m_lineRenderer = null;
	[SerializeField] FloatRef m_springPower = null;

	PhysicsBody BodyAnchor { get; set; } = null;

	private void Update()
	{
		m_lineRenderer.enabled = (BodyAnchor != null);
		if (BodyAnchor != null)
		{
			// using the line renderer, draw a line from the body anchor position to the mouse world point
			Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			m_lineRenderer.SetPosition(0, BodyAnchor.Position);
			m_lineRenderer.SetPosition(1, position);
		}
	}

	void Create(PhysicsBody bodyA, PhysicsBody bodyB, float restLength, float springPower)
	{
		PhysicsSpringJoint joint = new PhysicsSpringJoint();

		// set the joint bodies, the rest length and k
		joint.BodyA = bodyA;
		joint.BodyB = bodyB;
		joint.RestLength = restLength;
		joint.SpringPower = springPower;
		// add the joint to the physics world joints
		m_physicsWorld.joints.Add(joint);
	}

	public override void StartEvent()
	{
		// get a physics body using the Physics World GetPhysicsBodyFromPosition
		PhysicsBody body = PhysicsWorld.BodyAtPosition(Input.mousePosition);
		// if not null
		if (body != null)
		{
			// set the bodyAnchor to the body and set active to true
			BodyAnchor = body;
		}
	}

	public override void StopEvent()
	{
		if (BodyAnchor != null)
		{
			// get a physics body using the Physics World GetPhysicsBodyFromPosition
			PhysicsBody body = PhysicsWorld.BodyAtPosition(Input.mousePosition);

			// if not null and the body isn’t the body anchor
			if (body != null && BodyAnchor != body)
			{
				// set float restLength to (bodyAnchor – body) magnitude
				float restLength = (BodyAnchor.Position - body.Position).magnitude;
				// call the create function passing (bodyAnchor, body, restLength and m_k)
				Create(BodyAnchor, body, restLength, m_springPower.value);
			}
		}
		// set bodyAnchor to null and set active to false
		BodyAnchor = null;
	}
}
