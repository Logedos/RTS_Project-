using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] private float _normalMoveSpeed;
    [Range(0,1)]
    [SerializeField] private float _fastMoveSpeed;
    [Range(0,1)]
    [SerializeField] private float _moveTime;

    [SerializeField] private float _rotationAmount;

    private Vector3 _newPosition;
    private Quaternion _newRotation;

    private float _moveSpeed;

    private Vector3 rotateStartPosition;
    private Vector3 rotateCurrentPosition;

    private void Start()
    {
        _newPosition = transform.position;
        _newRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        HandleMovementInput();
        HandleMouseInput();
    }
    
    private void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _moveSpeed = _fastMoveSpeed;
        }
        else
        {
            _moveSpeed = _normalMoveSpeed;
        }
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _newPosition += (transform.forward * _moveSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _newPosition += (transform.forward * -_moveSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _newPosition += (transform.right * _moveSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _newPosition += (transform.right * -_moveSpeed);
        }

        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.fixedDeltaTime / _moveTime);
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;
            Vector3 difference = rotateStartPosition - rotateCurrentPosition;
            rotateStartPosition = rotateCurrentPosition;

            transform.rotation *= Quaternion.Euler(Vector3.up * (-difference.x / 4f));
        }
    }
}
