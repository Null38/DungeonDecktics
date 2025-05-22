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
        if (controller.TargetPos.HasValue)
            Move();
    }

    private void Move()
    {
        Vector3 target = controller.TargetPos.Value;
        

        // 4) 이동
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            DataManager.Speed * Time.fixedDeltaTime
        );


        // 5) 목표 지점 도달 여부 판단
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            // 정확히 스냅
            transform.position = target;
            controller.NextStep();
        }
    }
}
