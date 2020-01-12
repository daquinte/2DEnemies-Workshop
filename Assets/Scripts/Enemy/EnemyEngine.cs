using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngine : MonoBehaviour
{

    /// <summary>
	/// Referencia al objetivo al que queremos llegar
	/// </summary>
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
	public List<MovementBehaviour> enemyBehaviours;

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
		float distance = (TargetPosition() - transform.position).magnitude;
		isMoving = distance > m_StopDistance;
	}


	public Vector3 TargetPosition()
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
		//TODO: THIS
	}

}
