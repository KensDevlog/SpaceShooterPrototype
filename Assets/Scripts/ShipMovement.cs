using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]   
public class ShipMovement : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;

    private Vector2 _moveInput;
    private bool _isShooting = false;

    private float _shotInterval = 0.333f;
    private float _timeToNextShot = -1000;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue inputValue)
    {
        _moveInput = inputValue.Get<Vector2>();
    }

    public void OnAttack(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            _isShooting = true;
        }
        else
        {
            _isShooting=false;
            Debug.Log("Shooting false");
        }
    }

    private void FixedUpdate()
    {
        // Convert 2D input to 3D movement
        // X axis = left/right (strafe)
        // Y axis = forward/backward
        Vector3 movement = new Vector3(_moveInput.x, 0f, _moveInput.y);

        // Apply movement relative to ship's current rotation
        Vector3 moveDirection = transform.TransformDirection(movement);

        // Set velocity for crisp movement
        _rb.linearVelocity = moveDirection * _speed;
    }

    private void Update()
    {
        if (_isShooting && Time.time > _timeToNextShot)
        {
            _timeToNextShot = Time.time + _shotInterval;
            Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
        }
    }
}