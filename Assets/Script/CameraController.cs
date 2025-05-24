using UnityEngine;

/// <summary>
/// 카메라를 플레이어에게 부드럽게 따라다니게 하거나,
/// 타겟이 없을 때는 키보드 패닝 및 마우스 휠 줌을 지원합니다.
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("Follow Settings")]
    [Tooltip("따라갈 대상. 비워두면 수동 패닝 모드로 전환됩니다.")]
    public Transform target;
    [Tooltip("대상과의 오프셋 (Z는 -10 권장)")]
    public Vector3 offset = new Vector3(0, 0, -10);
    [Tooltip("부드러운 따라가기 시간")]
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

    void LateUpdate()
    {
        // 1) 타겟이 지정되어 있으면 부드럽게 따라갑니다.
        if (target != null)
        {
            Vector3 desired = target.position + offset;
            Vector3 smoothed = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
            transform.position = smoothed;
        }
        else
        {
            // 2) 타겟이 없으면 키보드 입력으로 패닝
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector3 pos = transform.position + new Vector3(x, y, 0) * panSpeed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, panLimitMin.x, panLimitMax.x);
            pos.y = Mathf.Clamp(pos.y, panLimitMin.y, panLimitMax.y);
            transform.position = pos;
        }

        // 3) 마우스 휠로 줌 인/아웃
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            Camera cam = GetComponent<Camera>();
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scroll * scrollSpeed, minZoom, maxZoom);
        }
    }
}
