using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ToggleFollow() 호출 시 수동 드래그 패닝 기능을 활성화.
/// 기본적으로 카메라는 target을 따라다니지만,
/// followEnabled=false 상태에서 드래그로 맵을 이동하여 볼 수 있음.
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("Follow")]
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float smoothTime = 0.3f;
    private Vector3 velocity;

    [Header("Drag Pan")]
    public float panSpeed = 1f;
    public Vector2 panLimitMin = new Vector2(-50, -50);
    public Vector2 panLimitMax = new Vector2(50, 50);
    private bool followEnabled = true;
    private bool isDragging;
    private Vector3 lastMousePosition;

    
    /// <summary>
    /// 따라오기 모드 On/Off 토글 버튼
    /// </summary>
    
    public void ToggleFollow()
    {
        followEnabled = !followEnabled;
        Debug.Log($"[CameraController] followEnabled={followEnabled}");
    }

    void LateUpdate()
    {
        if (followEnabled && target != null)
        {
            //타겟추적카메라
            Vector3 desired = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
        }
        else
        {                      
            // 드래그 시작
            if (Input.GetMouseButtonDown(0) )
            {
                isDragging = true;
                lastMousePosition = Input.mousePosition;
            }
            // 드래그 중
            if (Input.GetMouseButton(0) && isDragging)
            {
                Vector3 delta = Input.mousePosition - lastMousePosition;
                transform.position -= new Vector3(delta.x, delta.y, 0) * (panSpeed * Time.deltaTime);
                lastMousePosition = Input.mousePosition;
                // 자유전환시 카메라가 나가는걸 방지
                Vector3 p = transform.position;
                p.x = Mathf.Clamp(p.x, panLimitMin.x, panLimitMax.x);
                p.y = Mathf.Clamp(p.y, panLimitMin.y, panLimitMax.y);
                transform.position = p;
            }
            // 드래그 종료
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
        }
    }
}
