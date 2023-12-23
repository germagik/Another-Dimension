using Unity.VisualScripting;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float speed = 10f;
    protected Warrior warrior;
    void Start() => warrior = Warrior.Instance();
    void Update()
    {
        if (warrior.IsDestroyed()) {
            return;
        }
        Vector3 direction = (warrior.transform.position - transform.position) * speed * Time.deltaTime;
        transform.position += new Vector3(direction.x, direction.y);
    }
}
