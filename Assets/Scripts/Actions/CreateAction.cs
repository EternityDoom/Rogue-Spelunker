﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAction : Action
{
	bool active { get; set; } = false;
	float timer { get; set; } = 0.0f;

	[SerializeField] GameObject m_gameObject = null;

	[SerializeField] EmissionEnumRef m_emission = null;
	[SerializeField] BodyTypeEnumRef m_bodyType = null;
	[SerializeField] FloatRef m_velocity = null;
	[SerializeField] FloatRef m_damping = null;
	[SerializeField] FloatRef m_radius = null;

	public override void StartEvent()
	{
		active = true;
	}

	public override void StopEvent()
	{
		active = false;
	}

	void Create(Vector2 position, Vector2 velocity)
	{
		GameObject go = Instantiate(m_gameObject, position, Quaternion.identity);
		PhysicsBody body = go.GetComponent<PhysicsBody>();
		body.Type = m_bodyType.type;
		body.Damping = m_damping.value;

		((CircleShape)body.Shape).radius = m_radius.value;
		body.Mass = body.Shape.ComputeMass(2.0f);

		body.ApplyForce(velocity, PhysicsBody.eForceMode.VELOCITY);
		body.World = m_physicsWorld;

		m_physicsWorld.bodies.Add(body);
	}
	private void Update()
	{
		if (!active) return;

		switch (m_emission.type)
		{
			case EmissionEnumRef.eType.Single:
				{
					Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					Vector2 velocity = Random.insideUnitCircle.normalized * m_velocity.value;
					Create(position, velocity);
					active = false;
				}
				break;

			case EmissionEnumRef.eType.Burst:
				{
					for (int i = 0; i < 20; i++)
					{
						Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
						Vector2 velocity = Random.insideUnitCircle.normalized * m_velocity.value;
						Create(position, velocity);
					}
					active = false;
				}
				break;

			case EmissionEnumRef.eType.Stream:
				{
					float rateTime = 1.0f / 30.0f;
					timer = timer + Time.deltaTime;
					while (timer > rateTime)
					{
						Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
						Vector2 velocity = Random.insideUnitCircle.normalized * m_velocity.value;
						Create(position, velocity);
						timer = timer - rateTime;
					}
				}
				break;

			default:
				break;
		}
	}

}
