using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] protected Effect effect;
    public void Perform()
    {
        Instantiate(effect, transform.position, transform.rotation);
    }
}
