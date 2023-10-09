using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Representa a cualquier personaje, del jugador o enemigo.
 * Implementa la lógica para:
 * -> Moverse en todas direcciones, girando sus partes y coordinando las animaciones.
 * -> Apuntar y ejecutar ataques, coordinando las animaciones.
 * -> Recibir daño, calculando la absorción de defensa y coordinando las animaciones.
 */
public class Character : MonoBehaviour
{
    public float speed = 10f;
    public float range = 1f;
    public float maxLife = 100f; 
    private float life;
    public Attack[] attacks = new Attack[0]; 
    private Part[] parts;
    void Reset()
    {
        attacks = GetComponentsInChildren<Attack>();
        life = maxLife;
    }
    void OnValidate ()
    {
        Reset();
    }
    public void MoveTo(Vector3 direction, float speedFactor = 1f)
    {
        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }  
        transform.position += direction * (speed / speedFactor) * Time.deltaTime;
    }
    public void PointTo(Vector3 direction)
    {
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].transform.up = -direction;
        }
    }
    public virtual void Start()
    {
        parts = GetComponentsInChildren<Part>();
    } 
    public void ReceiveDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }   
}
