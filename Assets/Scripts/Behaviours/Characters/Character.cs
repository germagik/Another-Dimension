using UnityEngine;
using UnityEngine.UI;
using Utils;

/*
 * Representa a cualquier personaje, del jugador o enemigo.
 * Define la interfaz para:
 * -> Moverse en alguna dirección, girando sus partes y coordinando las animaciones.
 * -> Apuntar a alguna dirección, moviendo cada una de sus partes en su propio eje.
 * -> Recibir daño, calculando la absorción de defensa y coordinando las animaciones.
 */
public abstract class Character : MonoBehaviour
{
	[SerializeField] protected float speed = 10f;
	[SerializeField] protected float runningFactor = 1.5f;
	[SerializeField] protected float range = 1f;
	[SerializeField] protected float maxLife = 100f;
	[SerializeField] protected float maxStamina = 100f;
	[SerializeField] protected float staminaLoss = 1f;
	[SerializeField] protected float staminaGain = 1f;
	[SerializeField] protected float power = 1f;
	[SerializeField] protected Attack[] attacks = new Attack[0];
	[SerializeField] protected KnownTags targetTag;
	public TextMesh infoText;
	protected Vector3 deltaMovement = Vector3.zero;
	public bool IsMoving {
		get
		{
			return deltaMovement.magnitude > 0;
		}
	}
	protected Vector3 lastPosition;
	public Image lifeFill;
	private float life;
	protected float Life
	{
		get
		{
			return life;
		}
		set
		{
			if (value > maxLife)
			{
				life = maxLife;
			}
			else
			{
				life = value;
			}
			UpdateLife();
		}
	}
	protected bool running = false;
	public Image staminaFill;
	private float stamina;
	protected float Stamina
	{
		get
		{
			return stamina;
		}
		set
		{
			if (value > maxStamina)
			{
				stamina = maxStamina;
			}
			else
			{
				stamina = value;
			}
			UpdateStamina();
		}
	}
	public Attack CurrentAttack
	{
		get
		{
			foreach (Attack attack in attacks)
			{
				if (attack.IsPerforming)
				{
					return attack;
				}
			}
			return null;
		}
	}
	protected Part[] parts;
	protected virtual void Reset()
	{
		attacks = GetComponentsInChildren<Attack>();
		parts = GetComponentsInChildren<Part>();
		infoText = GetComponentInChildren<TextMesh>();
	}
	protected virtual void OnValidate()
	{
		Reset();
	}
	protected virtual void Start()
	{
		Reset();
		Stamina = maxStamina;
		Life = maxLife;
		lastPosition = transform.position;
	}

	protected virtual void UpdateLife()
	{
		lifeFill.fillAmount = Life / maxLife;
	}
	protected virtual void UpdateStamina()
	{

		if (staminaFill) {
			staminaFill.fillAmount = Stamina / maxStamina;
		}
	}

	protected virtual void Update()
	{
		if (!running && Stamina < maxStamina) {
			Stamina += staminaGain * Time.deltaTime;
		}
	}

	protected void LateUpdate()
	{
		deltaMovement = transform.position - lastPosition;
		lastPosition = transform.position;
	}

	protected void MoveTo(Vector3 direction)
	{
		if(direction.magnitude > 1)
		{
			direction = direction.normalized;
		}
		Attack attack = CurrentAttack;
		if (attack != null)
		{
			transform.position += direction * (speed / attack.SlowdownFactor) * Time.deltaTime;
		}
		else if(running && (staminaLoss * Time.deltaTime) < Stamina)
		{
			transform.position += direction * speed * runningFactor * Time.deltaTime;
			Stamina -= staminaLoss * Time.deltaTime;
		}
		else
		{
			transform.position += direction * speed * Time.deltaTime;
		}
	}

	protected void PointTo(Vector3 direction)
	{
		for(int i = 0; i < parts.Length; i++)
		{
			parts[i].PointTo(-direction); // Negativo para que las figuras iniciales deban apuntar hacia abajo
		}
	}
	protected void PerformAttack(int index)
	{
		if(!CurrentAttack && attacks.Length > 0 && attacks[index].IsReady)
		{
			attacks[index].Perform(targetTag);
			foreach (Part part in parts)
			{
				part.Attack();
			}
		}
	}

	public void ReceiveDamageFrom(Character anotherCharacter, float damage)
	{
		Life = Life - (damage * anotherCharacter.power);
		if(Life <= 0)
		{
			Destroy(gameObject);
		}
	}

	public void ReceiveDamage(float damage)
	{
		Life -= damage;
		if(Life <= 0)
		{
			Destroy(gameObject);
		}
	}

	public void Heal(float factor)
	{
		Life += factor;
	}
}
