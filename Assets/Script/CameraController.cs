using UnityEngine;

/// 카메라를 플레이어 등 지정된 대상에 부드럽게 따라다니게 하거나,
/// 타겟 잠금(On/Off) 기능 및 수동 패닝과 줌 기능을 지원함.
/// UI 버튼을 통해 토글을 실행함.

public class CameraController : MonoBehaviour
{
    [Header("Follow Settings")]
    [Tooltip("따라갈 대상. 비워두면 수동 패닝 모드로 전환됩니다.")]
    public Transform target;
    private Transform initialTarget;

    [Tooltip("대상과의 오프셋 (Z는 -10 권장)")]
    public Vector3 offset = new Vector3(0, 0, -10);
    [Tooltip("부드러운 따라가기 시간 (초)")]
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    [Header("Pan Settings")]
    [Tooltip("수동 패닝 속도")]
    public float panSpeed = 20f;
    [Tooltip("패닝 가능한 최소/최대 X,Y 좌표")]
    public Vector2 panLimitMin = new Vector2(-50, -50);
    public Vector2 panLimitMax = new Vector2(50, 50);

    [Header("Zoom Settings")]
    [Tooltip("휠 스크롤 감도")]
    public float scrollSpeed = 5f;
    [Tooltip("최소/최대 Orthographic Size")]
    public float minZoom = 5f;
    public float maxZoom = 15f;

    // Follow 모드 On/Off 상태
    private bool followEnabled = true;

    void Awake()
    {
        // 초기 할당된 타겟을 저장해 두어, 잠금 해제 시 복구에 사용
        initialTarget = target;
    }

    /// UI 버튼에서 호출할 타겟 토글 함수
    
    public void ToggleFollow()
    {
        followEnabled = !followEnabled;
        if (followEnabled && target == null) target = initialTarget;
        Debug.Log($"[CameraController] ToggleFollow called. followEnabled = {followEnabled}");
    }

    void LateUpdate()
    {
        // Follow 모드가 켜져 있고, 타겟이 있으면 부드럽게 따라갑니다.
        if (followEnabled && target != null)
        {
            Vector3 desired = target.position + offset;
            Vector3 smoothed = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
            transform.position = smoothed;
        }
        else
        {
            // 수동 패닝 로직 (모바일 조작 시 가상 조이스틱이나 화면 드래그로 확장 가능)
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector3 pos = transform.position + new Vector3(x, y, 0) * panSpeed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, panLimitMin.x, panLimitMax.x);
            pos.y = Mathf.Clamp(pos.y, panLimitMin.y, panLimitMax.y);
            transform.position = pos;
        }

        // 줌 로직 (터치 핀치 제스처로 확장 가능)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            Camera cam = GetComponent<Camera>();
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scroll * scrollSpeed, minZoom, maxZoom);
            Debug.Log($"[CameraController] Zoom changed: {cam.orthographicSize}");
        }
    }
}
