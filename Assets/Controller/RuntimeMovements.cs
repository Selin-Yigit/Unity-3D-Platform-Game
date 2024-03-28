using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Bu bi tag. Bu tag içine bir tip alabilir. Yazılmasının nedeni varsa çalışmasını istemem yoksa error vericek.
// Bu tagler olmadan yazılan fonksiyon kesinlikle çalışmamalıdır.
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class RuntimeMovements : MonoBehaviour
{
    private Movements _input;
    private CharacterController _controller;
    private Animator _animator;
    [SerializeField] private float fraction;
    private float speed;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<Movements>();
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        Move();
    }

    private void Move()
    {
        speed = _animator.GetFloat("speed");
        float x = _input.moveValue.x * _input.moveSpeed / fraction;
        float z = _input.moveValue.y * _input.moveSpeed / fraction;
        _controller.Move(new Vector3(x, 0f, z));
        //_animator.SetFloat("speed", Mathf.Abs(_controller.velocity.x) + Mathf.Abs(_controller.velocity.z)); //=> sadece ileri gidiyor yürüme bozuluyor.
        speed = _controller.velocity.x + _controller.velocity.z;
        _animator.SetFloat("speed", speed);
        //_animator.SetFloat("speed", Mathf.Abs(speed));
        Debug.Log(_controller.velocity.x + " " + _controller.velocity.z);


    }

}
