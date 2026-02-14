using UnityEngine;


public class CameraTargetController : MonoBehaviour
{
    [Header("========== Mouse Settings ==========")]
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float minVerticalAngle = -30f;
    [SerializeField] private float maxVerticalAngle = 60f;

    [Header("========== State ==========")]
    [SerializeField] private float currentYaw = 0f; // Y
    [SerializeField] private float currentPitch = 20f; // X

    [Header("========== Smoothing ==========")]
    [SerializeField] private float rotationSmoothing = 5f;
    private float _targetYaw, _targetPitch;

    
    void Awake()
    {
        Vector3 euler = transform.localRotation.eulerAngles;
        currentYaw = euler.y;
        currentPitch = NormalizeAngle(euler.x);
        
        currentPitch = Mathf.Clamp(
            currentPitch, 
            minVerticalAngle, 
            maxVerticalAngle);
        
        transform.localRotation = Quaternion.Euler(
            currentPitch, 
            currentYaw, 
            0f);

        _targetYaw = currentYaw;
        _targetPitch = currentPitch;
    }


    void Update()
    {
        if (InputManager.Instance == null) return;

        Vector2 lookInput = InputManager.Instance.GetLookInput();

        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        currentYaw += mouseX;
        currentPitch -= mouseY;
        currentPitch = Mathf.Clamp(
            currentPitch,
            minVerticalAngle,
            maxVerticalAngle);

        transform.localRotation = Quaternion.Euler(
            currentPitch,
            currentYaw,
            0f);

        ApplyLookInput(lookInput);
    }


    public void SetMouseSensitivity(float sensitivity)
    {
        mouseSensitivity = Mathf.Clamp(sensitivity, 0.1f, 10f);
    }


    public float GetMouseSensitivity() => mouseSensitivity;


    private static float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }


    private void ApplyLookInput(Vector2 lookInput)
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        // Update target angles based on input
        _targetYaw += mouseX;
        _targetPitch -= mouseY;
        _targetPitch = Mathf.Clamp(
            _targetPitch,
            minVerticalAngle,
            maxVerticalAngle);

        // Smooth interpolation towards target angles
        currentYaw = Mathf.LerpAngle(
            currentYaw,
            _targetYaw,
            rotationSmoothing * Time.deltaTime);
        currentPitch = Mathf.Lerp(
            currentPitch,
            currentYaw, 0f);
    }
}
