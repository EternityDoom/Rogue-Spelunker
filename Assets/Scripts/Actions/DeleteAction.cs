using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeleteAction : Action
{
	public override void StartEvent()
	{
		PhysicsBody body = PhysicsWorld.BodyAtPosition(Input.mousePosition);
		if (body != null)
		{
			List<PhysicsJoint> deletion = new List<PhysicsJoint>();
			foreach(PhysicsJoint joint in m_physicsWorld.joints)
			{
				if (joint.BodyA == body || joint.BodyB == body)
				{
					deletion.Add(joint);
				}
			}
			deletion.ForEach(joint => m_physicsWorld.joints.Remove(joint));
			m_physicsWorld.bodies.Remove(body);
			Destroy(body.gameObject);
		}
	}

	public override void StopEvent()
	{
	}

}
