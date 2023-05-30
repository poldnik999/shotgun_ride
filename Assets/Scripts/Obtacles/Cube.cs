using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static Unit_HP;

public class Cube : MonoBehaviour
{
    public Unit_HP ObtacleUnit;
    public int HP;
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        ObtacleUnit = new Unit_HP();
        ObtacleUnit.HealthPoint = HP;
        ObtacleUnit.Name = name;
    }

    // Update is called once per frame
    void Update()
    {
        if (ObtacleUnit.lifeStatus == false)
            Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(10);
        }

    }
}
