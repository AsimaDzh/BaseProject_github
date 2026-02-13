using UnityEngine;


public class CameraTargetController : MonoBehaviour
{
    [Header("========== Mouse Settings ==========")]
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float minVerticalAngle = -30f;
    [SerializeField] private float maxVerticalAgle = 60f;

    [Header("========== State ==========")]
    [SerializeField] private float currentYaw = 0f; // Y
    [SerializeField] private float currentPitch = 20f; // X

    
    void Awake()
    {
        Vector3 euler = transform.localRotation.eulerAngles;
        currentYaw = euler.y;
        currentPitch = NormalizeAngle(euler.x);
        
        currentPitch = Mathf.Clamp(
            currentPitch, 
            minVerticalAngle, 
            maxVerticalAgle);
        
        transform.localRotation = Quaternion.Euler(
            currentPitch, 
            currentYaw, 
            0f);
    }


    void Update()
    {
        
    }
}
