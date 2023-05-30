using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Gun_shoot : MonoBehaviour
{
    public Camera camera;
    public Transform aimTransform;
    public Vector3 AimPoint { get; set; }
    public Vector3 AimNormal { get; set; }

    public Transform Player_rotate;
    public Transform gunTransform;

    public GameObject Bullet;

    private Quaternion targetRotation;
    

    // Перезарядка и кол-во патронов в обойме
    public int AmmoCount = 30;      // Кол-во патронов
    public float ReloadTime = 3;    // Время перезарядки
    public float RateOfFire = 0.1f; // Скорострельность

    public bool isActiveReload = false;
    private bool isActiveShoot = false;
    public int AmmoLeft;
    private float Timer;
    void Start()
    {
        AmmoLeft = AmmoCount;
    }

    // Update is called once per frame
    void Update()
    {
        LookOnCursor();
        //Aiming();
        
    }
    private void FixedUpdate()
    {
        isActiveShoot = false;
        if (Input.GetKey(KeyCode.Mouse0))   isActiveShoot = true;
        if (Input.GetKey(KeyCode.R))        isActiveReload = true;

        if (isActiveReload)
        {
            Timer += Time.deltaTime;
            if (Timer > ReloadTime)
            {
                AmmoLeft = AmmoCount;
                Timer = 0;
                isActiveReload = false;
            }
        }
        else if (isActiveShoot)
        {
            Timer += Time.deltaTime;
            if (Timer > RateOfFire)
            {
                Shoot();
                Timer = 0;
            }
        }
    }

    void Shoot()
    {
        if(AmmoLeft > 0)
        {
            GameObject bullet = Instantiate(Bullet, gunTransform.position, gunTransform.rotation);
            bullet.transform.rotation = targetRotation;
            Vector3 vct = aimTransform.position - Player_rotate.position;
            bullet.gameObject.GetComponent<Rigidbody>().AddForce(vct * 100);
            Destroy(bullet, 2);
            AmmoLeft--;
        }
    }
    void LookOnCursor()
    {       //заставляет персонажа следить за курсором мышки 		
        Plane playerPlane = new Plane(Vector3.up, Player_rotate.position);
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        float hitdist = 0;
        if (playerPlane.Raycast (ray, out hitdist)) 
        {
            Vector3 targetPoint = ray.GetPoint (hitdist);
            targetRotation = Quaternion.LookRotation (targetPoint - Player_rotate.position);
            Player_rotate.rotation = Quaternion.Normalize(targetRotation);
            Player_rotate.rotation *= Quaternion.Euler(0f, 90f, 0f);
            gunTransform.rotation = Quaternion.Normalize(targetRotation);
            //Player_rotate.rotation = Quaternion.Slerp (Player_rotate.rotation, targetRotation, 100 * Time.deltaTime); 
        } 	
    }
}
