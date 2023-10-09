using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
 
    private Warrior warrior;
    public float speed = 10f;
    void Start()
    {
        warrior = Warrior.Instance();
    }
    void Update()
    {
      
        Vector3 direction = (warrior.transform.position - transform.position) * speed * Time.deltaTime;
        transform.position += new Vector3(direction.x, direction.y);
    }
}
