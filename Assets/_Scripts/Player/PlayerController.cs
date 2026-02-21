using UnityEngine;

/// <summary>
/// We are doing strafe movement.
/// Controller will controll player character
/// </summary>

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("========== References ==========")]
    [Tooltip("Player stats component.")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform cameraTransform;
    [Tooltip("Root transform of the visual model")]
    [SerializeField] private Transform visualRoot;

    [Header("========== Movement & Physics ==========")]
    [Tooltip("Gravity value.")]
    [SerializeField] private float gravity = -9.81f;
    [Tooltip("Small downward velocity to keep the character grounded.")]
    [SerializeField] private float groundedGravity = -2f;
    [SerializeField] private float sprintMultiplier = 1.5f;

    private CharacterController _charControl;
    private Vector3 _verticalVelocity;
    private bool _isGrounded;


    private void Awake()
    {
        _charControl = GetComponent<CharacterController>();

        if (playerStats == null ) 
            playerStats = GetComponent<PlayerStats>();
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        if (InputManager.Instance == null) return;
    }


    private void Update()
    {
        HandleMovement();
        HandleJump();

        InputManager.Instance.ResetButtonFlags();
    }


    private void HandleMovement()
    {
        Vector2 _moveInput = InputManager.Instance.MoveInput;
        Vector3 _moveDirection = Vector3.zero;

        if (_moveInput.sqrMagnitude > 0.001f && cameraTransform != null )
        {
            Vector3 _forward = cameraTransform.forward;
            _forward.y = 0f;
            _forward.Normalize();

            Vector3 _right = cameraTransform.right;
            _right.y = 0f;
            _right.Normalize();

            _moveDirection = _forward * _moveInput.y + _right * _moveInput.x;
            _moveDirection.Normalize();
        }

        float _speed = 5f;
        float _rotationSpeed = 720f;

        if (playerStats != null && playerStats.playerData != null)
        {
            _speed = playerStats.playerData.moveSpeed;
            _rotationSpeed = playerStats.playerData.rotationSpeed;
        }

        if (InputManager.Instance.IsSprintHeld()) 
            _speed *= sprintMultiplier;

        Vector3 _horizontalVelocity = _moveDirection * _speed;
        _isGrounded = _charControl.isGrounded;

        if (_isGrounded && _verticalVelocity.y < 0f)
            _verticalVelocity.y = groundedGravity;

        _verticalVelocity.y += gravity * Time.deltaTime;
        
        Vector3 _velocity = _horizontalVelocity + _verticalVelocity;
        _charControl.Move(_velocity * Time.deltaTime);

        if (cameraTransform != null && visualRoot != null)
        {
            Vector3 _camForward = cameraTransform.forward;
            _camForward.y = 0f;
            _camForward.Normalize();

            if (_camForward.sqrMagnitude > 0.001f)
            {
                Quaternion _targetRotation = Quaternion.LookRotation(_camForward);
                visualRoot.rotation = Quaternion.Slerp(
                    visualRoot.rotation,
                    _targetRotation,
                    _rotationSpeed * Mathf.Deg2Rad * Time.deltaTime);
            }
        }
    }


    private void HandleJump()
    {
        if (!_isGrounded) return;

        if (InputManager.Instance.IsJumpPressed())
        {
            float _jumpForce = 5f;

            if(playerStats != null && playerStats.playerData != null)
                _jumpForce = playerStats.playerData.jumpForce;

            _verticalVelocity.y = Mathf.Sqrt(_jumpForce * -2f * gravity);
        }
    }
}
