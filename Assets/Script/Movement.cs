using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Movement : MonoBehaviour
{
    private Controller controller;

    [Header("Collision Layers")]
    [Tooltip("캐릭터(플레이어·적)가 올라가는 Layer")]
    [SerializeField] private LayerMask unitLayerMask;
    [Tooltip("벽으로 사용할 Tilemap/Collider가 적용된 Layer")]
    [SerializeField] private LayerMask wallLayerMask;

    [Header("Movement Settings")]
    [Tooltip("이동 속도 (유닛 단위/sec)")]
    [SerializeField] private float moveSpeed = 5f;
    [Tooltip("충돌 검사 반지름 (유닛 크기보다 약간 작게)")]
    [SerializeField] private float checkRadius = 0.15f;

    private void Awake()
    {
        controller = GetComponent<Controller>();
    }

    void FixedUpdate()
    {        
        // 1) TargetPos가 없으면(=이동할 경로가 없으면) 아무것도 하지 않음
        Vector3? tgt = controller.TargetPos;
        if (!tgt.HasValue)
            return;

        // 2) 다음 프레임에 이동할 위치 계산
        Vector3 target = tgt.Value;
        Vector3 nextPos = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.fixedDeltaTime
        );

        // 3) 벽 또는 다른 유닛과 겹치면, 해당 스텝을 건너뛰고 NextStep() 호출
        if (IsBlocked(nextPos))
        {
            controller.NextStep();
            return;
        }

        // 4) 실제 이동
        transform.position = nextPos;

        // 5) 목표 지점 도달 여부 판단
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            // 정확히 스냅
            transform.position = target;
            controller.NextStep();
        }
    }

    /// <summary>
    /// 해당 위치에 벽(wallLayerMask) 또는 다른 유닛(unitLayerMask)이 있으면 true 반환
    /// </summary>
    private bool IsBlocked(Vector3 pos)
    {
        // 1) 벽 충돌 검사
        if (Physics2D.OverlapCircle(pos, checkRadius, wallLayerMask))
            return true;

        // 2) 유닛 충돌 검사 (자기 자신은 무시)
        Collider2D hit = Physics2D.OverlapCircle(pos, checkRadius, unitLayerMask);
        if (hit != null && hit.gameObject != gameObject)
            return true;

        return false;


    }

}
