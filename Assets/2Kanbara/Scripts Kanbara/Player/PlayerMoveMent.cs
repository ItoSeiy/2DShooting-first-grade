using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveMent : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _lateSpeed;
    Vector2 _dir;
    bool _isLateMode = false;
    Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        switch (_isLateMode)
        {
            case false:
                _rb.velocity = _dir * _speed;
                break;
            case true:
                _rb.velocity = _dir * _lateSpeed;
                break;
        }
    }

    public void NormalMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        _dir = new Vector3(inputMovement.x, inputMovement.y, 0);
    }

    public void LateMoveMent(InputAction.CallbackContext value)
    {
        _isLateMode = true;
        if (value.canceled)
        {
            _isLateMode = false;
        }
    }
}
