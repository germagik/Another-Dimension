using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] protected float range = 1f;
    [SerializeField] protected float maxLife = 100f;
    protected float life;
    protected Part[] parts;
    protected virtual void Reset() {
        life = maxLife;
        parts = GetComponentsInChildren<Part>();
    }
    protected virtual void OnValidate()
    {
        Reset();
    }

    protected void MoveTo(Vector3 direction, float speedFactor = 1)
    {
        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }
        transform.position += direction * (speed / speedFactor) * Time.deltaTime;
    }

    protected void PointTo(Vector3 direction)
    {
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].transform.up = -direction; // Negativo para que las figuras iniciales deban apuntar hacia abajo
        }
    }
}
