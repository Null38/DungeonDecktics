using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Movement : MonoBehaviour
{
    private Controller controller;

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
            DataManager.Speed * Time.fixedDeltaTime
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
        Collider2D hit = Physics2D.OverlapPoint(pos, DataManager.UnPassableLayer);
        if (hit != null && hit.gameObject != gameObject)
            return true;

        return false;


    }

}
