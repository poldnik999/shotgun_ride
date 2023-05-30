using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamageable
{
    void ApplyDamage(int damageValue);
}
public class Unit_HP
{
    // �������� �� ���������
    private string name = "Unit";
    private int healthPoint = 100;
    public bool lifeStatus = true; // ������ ����� �������

    public int HealthPoint
    {
        // ���� �� ������� < 0, �� � ���� �������� ������ �� "�������"
        set
        {
            if (healthPoint < 0)
                lifeStatus = false;
            else
                healthPoint = value;
        }
        get { return healthPoint; }
    }
    public string Name
    {
        set { name = value; }
        get { return name; }
    }
    public void Damage(int dmg)
    {
        this.HealthPoint -= dmg;
    }

    // � ������� � ������ update �������� ������ ����� �������, ���� �� �����, �� ��������� ������ ����� Destroy()

    // �������� �������� ���:                                // ��� ���������� ��������� �� ����, ����� �� ���� = ����� ����
    //Unit_HP Unit_01;                                       // ��� ������������ ���� � �����, � ���� ���������� ������� ��, ������� ����� ����  
    //void Start()                                           // Unit_HP bullet = collision.gameObject.GetComponent<Projectile>().bullet;     // Projectile ��� ���� ������ ����
    //{                                                      // Unit_01.Damage(bullet.healthPoint); 
    //    Unit_01 = new Unit_HP();                           // ����� ����� GetComponent �������� ������ �� ������� �������, ���� ��� �������� ��� public
    //    Unit_01.healthPoint = 120;
    //    Unit_01.Name = "Cube";
    //}
    //void Update()
    //{
    //    if (Unit_01.lifeStatus == false)
    //        Destroy(gameObject);
    //}
}

