using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_1 : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public float moveSpeed = 2;
    public float sideSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        

    }
    public float Speed = 10f;
    public float JumpForce = 300f;

    //��� �� ��� ���������� �������� �������� ��� "Ground" �� ���� ����������� �����
    private bool _isGrounded;
    private Rigidbody _rb;

    // �������� �������� ��� ��� �������� � ������� 
    // ���������� ������������ � FixedUpdate, � �� � Update
    void FixedUpdate()
    {
        MovementLogic();
        JumpLogic();
    }

    private void MovementLogic()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        _rb.AddForce(movement * Speed);
    }

    private void JumpLogic()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (_isGrounded)
            {
                _rb.AddForce(Vector3.up * JumpForce);

                // �������� �������� ��� � ����� �� ������ Vector3.up 
                // � �� �� ������ transform.up. ���� �������� ���� ��� 
                // ���� �������� -- ���, �� ��� ������ "����" ����� 
                // ����� �����������. �����, ������, ����...
                // �� ��� ����� ������ ������ � ���������� �����, 
                // ������ � Vector3.up
            }
        }
    }
}
