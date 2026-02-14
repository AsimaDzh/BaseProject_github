using Unity.Cinemachine;
using UnityEngine;

public class ThirdPersonFollowZoom : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cmCamera;
    [SerializeField] private float minCameraDistance = 2f;
    [SerializeField] private float maxCameraDistance = 10f;
    [SerializeField] private float zoomSpeed = 2f;

    private CinemachineThirdPersonFollow _follow;


    private void Awake()
    {
        if (cmCamera == null)
            cmCamera = GetComponent<CinemachineCamera>();
        if (cmCamera != null)
            _follow = cmCamera.GetCinemachineComponent(CinemachineCore.Stage.Body) as CinemachineThirdPersonFollow;
    }


    private void Update()
    {
        if (_follow == null) return;

        float _scroll = InputManager.Instance != null ? InputManager.Instance.GetZoomInput() : 0f;
        if (Mathf.Abs(_scroll) > 0.0001f)
        {
            _follow.CameraDistance = Mathf.Clamp(
                _follow.CameraDistance - _scroll * zoomSpeed,
                minCameraDistance,
                maxCameraDistance);
        }
    }
}
