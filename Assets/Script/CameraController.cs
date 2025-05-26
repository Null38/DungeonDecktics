using UnityEngine;

/// <summary>
/// 카메라 따라가기와 수동 드래그 패닝을 지원합니다.
/// ToggleFollow()로 따라오기 모드 On/Off 전환이 가능하며,
/// 수동 패닝 시 해상도·카메라 위치에 영향 받지 않는 월드 델타 계산으로
/// 부드럽고 일관된 이동을 제공함.
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target;
    public Vector3 offset = new(0, 0, -10f);
    public float smoothTime = 0.3f;
    private Vector3 velocity;

    [Header("Pan Settings")]
    public float panSpeed = 1f;
    public Vector2 panLimitMin = new(-50f, -50f);
    public Vector2 panLimitMax = new(50f, 50f);

    [Header("Zoom Settings")]
    public float scrollSpeed = 5f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    private bool followEnabled = true;
    private bool isDragging = false;
    private Vector3 lastMousePosition;
    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        PlayerController.Moved += () => followEnabled = true;
    }

    /// <summary>
    /// 자동추적모드 토글
    /// </summary>
    public void ToggleFollow()
    {
        followEnabled = !followEnabled;
        Debug.Log($"[CameraController] followEnabled={followEnabled}");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            followEnabled = false;
        }
    }

    void LateUpdate()
    {
        if (followEnabled && target != null)
        {
            Vector3 desired = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
            ClampPosition();
        }
        else
        {
            HandleDragPan();
        }

        HandleZoom();
    }

    private void HandleDragPan()
    {
        // 드래그 시작
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }
        // 드래그 중
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 currentMousePos = Input.mousePosition;
            Vector3 deltaPixels = lastMousePosition - currentMousePos;

            // 화면 픽셀 델타를 월드 델타로 변환
            float worldScreenHeight = cam.orthographicSize * 2f;
            float worldScreenWidth = worldScreenHeight * cam.aspect;
            Vector3 worldDelta = new Vector3(
                deltaPixels.x / Screen.width * worldScreenWidth,
                deltaPixels.y / Screen.height * worldScreenHeight,
                0f
            ) * panSpeed;

            transform.position += worldDelta;
            lastMousePosition = currentMousePos;

            ClampPosition();
        }
        // 드래그 종료
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scroll * scrollSpeed, minZoom, maxZoom);
        }
    }

    private void ClampPosition()
    {
        Vector3 p = transform.position;
        p.x = Mathf.Clamp(p.x, panLimitMin.x, panLimitMax.x);
        p.y = Mathf.Clamp(p.y, panLimitMin.y, panLimitMax.y);
        transform.position = p;
    }
}
