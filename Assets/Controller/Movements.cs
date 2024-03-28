using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movements : MonoBehaviour
{
    private Rigidbody rgd3D;
    private Animator _animator;
    private float speed;
    public Vector2 moveValue;
    public float moveSpeed = 10;

    private void Awake()
    {
        rgd3D = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Moving(InputAction.CallbackContext value)
    {
        speed = _animator.GetFloat("speed");
        if (value.performed)
        {
            // Bu ilk yazılan en basit hali. Daha spnra RuntimeMovements script'ini yazdık ve değeri orada okuyup hareketi gerçekleştirdik.

            //Debug.Log("performed");
            moveValue = value.ReadValue<Vector2>();
            _animator.SetBool("moving", true);
            //Debug.Log(moveValue.x + " and" + moveValue.y);
            //rgd3D.AddForce(new Vector3(moveValue.x * moveSpeed, 0f, moveValue.y * moveSpeed), ForceMode.Impulse);

        }
        if (value.canceled)
        {
            moveValue = value.ReadValue<Vector2>();
            _animator.SetBool("moving", false);
        }
    }
}
