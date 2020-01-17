using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the core of an enemy.
/// </summary>
public class EnemyEngine : MonoBehaviour
{

	[Tooltip("Objetivo que tendrá el enemigo. Si es null, se asignará el jugador")]
	public GameObject m_target = null;

	/// <summary>
	/// Distancia a la que consideramos que hemos alcanzado el objetivo
	/// </summary>
	[Tooltip("Distancia a la que consideramos que hemos alcanzado el objetivo")]
	public float m_StopDistance = 0.2f;

	/// <summary>
	/// Velocidad máxima
	/// </summary>
	[Tooltip("Velocidad máxima a la que puede ir el enemigo")]
	public float m_maxSpeed = 5.0f;

	/// <summary>
	/// Aceleración máxima
	/// </summary>
	public float m_maxAccel = 5.0f;

	/// <summary>
	/// Lista con los comportamientos que component el movimiento
	/// </summary>
	private List<MovementBehaviour> enemyBehaviours;

	/// <summary>
	/// ¿Nos estamos moviendo?
	/// </summary>
	private bool isMoving;

	/// <summary>
	/// Vector velocidad
	/// </summary>
	private Vector3 velocity;

	/// <summary>
	/// Vector de aceleración. Será actualizado por los steerings
	/// </summary>
	private Vector2 accel = Vector2.zero;

	private void Awake()
	{
		enemyBehaviours = new List<MovementBehaviour>();

		isMoving = false;
		if (m_target != null)
		{
			isMoving = true;
		}
		else
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if (player != null)
			{
				m_target = player;
				isMoving = true;
			}
		}
	}

	// Start is called before the first frame update
	void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
		if (isMoving)
			updatePosition();
		float distance = (GetTargetPosition() - transform.position).magnitude;
		isMoving = distance > m_StopDistance;
	}


	public Vector3 GetTargetPosition()
	{
		return m_target != null ? m_target.transform.position : Vector3.zero;
	}

	public void newTarget(GameObject target)
	{
		if (target != this.gameObject)
			m_target = target;
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
		if (accel.magnitude > m_maxAccel)
		{
			accel.Normalize();
			accel *= m_maxAccel;
		}
		Vector2 newVelocity = velocity;
		newVelocity += accel * Time.deltaTime;

		if (newVelocity.magnitude > m_maxSpeed)
		{
			newVelocity.Normalize();
			newVelocity *= m_maxSpeed;
		}
		velocity = new Vector3(newVelocity.x, newVelocity.y, 0.0f);
		transform.position += velocity * Time.deltaTime;

		// The enemy forward vector is facing up
		//transform.up = velocity.normalized;
		accel = Vector3.zero;
	}

	public Vector3 GetVelocity() { return velocity; }

	/// <summary>
	/// Flips the enemy
	/// </summary>
	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		//m_FacingRight = !m_FacingRight;

		transform.Rotate(0f, 180f, 0f);
	}

}
