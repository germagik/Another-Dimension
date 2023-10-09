using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private string targetTag;
    private float damage;
    public void Initialize(string initialTargetTag, float initialDamage)
    {
        targetTag = initialTargetTag;
        damage = initialDamage;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            Character character = collision.gameObject.GetComponent<Character>();
            character.ReceiveDamage(damage);
            Destroy(gameObject);
        }
    }
 
}
