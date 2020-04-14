using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAction : Action
{
	[SerializeField] LineRenderer m_lineRenderer = null;

	PhysicsBody BodySelect { get; set; } = null;

	private void Update()
	{
		m_lineRenderer.enabled = (BodySelect != null);
		if (m_lineRenderer.enabled)
		{
			Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			m_lineRenderer.SetPosition(0, BodySelect.Position);
			m_lineRenderer.SetPosition(1, position);

			if (BodySelect.Type == BodyTypeEnumRef.eType.Dynamic)
			{
				Vector2 force = PhysicsSpringJoint.SpringForce(position, BodySelect.Position, 0.01f, 30.0f);
				BodySelect.ApplyForce(force, PhysicsBody.eForceMode.IMPULSE);
			}
			else
			{
				BodySelect.Position = position;
			}
		}
	}

	public override void StartEvent()
	{
		BodySelect = PhysicsWorld.BodyAtPosition(Input.mousePosition);
	}

	public override void StopEvent()
	{
		BodySelect = null;
	}

}
