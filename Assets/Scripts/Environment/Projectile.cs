using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Unit_HP bullet;
    [SerializeField]
    private float speed;
    public int Test = 10;
    private Vector3 firePoint;
    // Start is called before the first frame update
    void Start()
    {
        bullet = new Unit_HP();
        bullet.Name = "bullet";
        bullet.HealthPoint = 10;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name != "Player")
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(bullet.HealthPoint);
            }

    }
}
