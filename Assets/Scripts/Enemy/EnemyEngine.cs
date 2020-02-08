using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the core of an enemy.
/// </summary>
public class EnemyEngine : MonoBehaviour
{

	[Tooltip("Objetivo que tendrá el enemigo. Si es null, se asignará el jugador")]
	public GameObject enemyTarget = null;

	[Tooltip("Distance in which we consider the enemy has reached the player")]
	public float stopDistance = 0.2f;

	[Tooltip("Maximum velocity in which an enemy can move")]
	public float maxVelocity = 5.0f;

	[Tooltip("Maximum acceleration that can be applied to an enemy")]
	public float maxAccel = 5.0f;

	/// <summary>
	/// List with every component that composes an enemy behaviour
	/// </summary>
	private List<MovementBehaviour> enemyBehaviours;

	/// <summary>
	/// ¿Is the enemy moving?
	/// </summary>
	private bool isMoving;

	/// <summary>
	/// ¿Is the enemy facing left?
	/// </summary>
	private bool isFacingLeft;

	/// <summary>
	/// Velocity vector that this enemy has
	/// </summary>
	private Vector3 velocity;

	/// <summary>
	/// Acceleration vector. It will be modified by the movement behaviours
	/// </summary>
	private Vector2 accel = Vector2.zero;

	private void Awake()
	{
		enemyBehaviours = new List<MovementBehaviour>();

		isMoving = false;
		isFacingLeft = true;
		if (enemyTarget != null)
		{
			isMoving = true;
		}
		else
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if (player != null)
			{
				enemyTarget = player;
				isMoving = true;
			}
		}
	}


    // Update is called once per frame
    void Update()
    {
		if (isMoving)
			updatePosition();
		float distance = (GetTargetPosition() - transform.position).magnitude;
		isMoving = distance > stopDistance;
	}


	public Vector3 GetTargetPosition()
	{
		return enemyTarget != null ? enemyTarget.transform.position : Vector3.zero;
	}

	public Vector3 GetVelocity() { return velocity; }
	public float GetMaxVelocity() { return maxVelocity; }

	public void newTarget(GameObject target)
	{
		if (target != this.gameObject)
			enemyTarget = target;
	}

	public void RegistrerBehaviour(MovementBehaviour mb)
	{
		enemyBehaviours.Add(mb);
	}

	private void updatePosition()
	{
		foreach (MovementBehaviour sb in enemyBehaviours)
		{
			accel += sb.GetMovement();
		}
		if (accel.magnitude > maxAccel)
		{
			accel.Normalize();
			accel *= maxAccel;
		}
		Vector2 newVelocity = velocity;
		newVelocity += accel * Time.deltaTime;

		if (newVelocity.magnitude > maxVelocity)
		{
			newVelocity.Normalize();
			newVelocity *= maxVelocity;
		}
		velocity = new Vector3(newVelocity.x, newVelocity.y, 0.0f);
		transform.position += velocity * Time.deltaTime;

		// The enemy forward vector is facing up
		//transform.up = velocity.normalized;
		accel = Vector3.zero;
	}

	/// <summary>
	/// Flips the enemy
	/// </summary>
	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		//m_FacingRight = !m_FacingRight;
		isFacingLeft = !isFacingLeft;
		transform.Rotate(0f, 180f, 0f);
	}

}
