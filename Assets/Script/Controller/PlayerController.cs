using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;
using System;

public class PlayerController : Controller, ITurnBased
{
    public static event Action Moved;
    // 애니메이션 컴포넌트
    [SerializeField] private Animator animator;
    private static readonly int HashWalk = Animator.StringToHash("IsWalking");

    private Vector3? target = null; 

    private bool touch = false;
    private bool isTouchMove = true;

    void Awake()
    {
        DataManager.player = this;
        var infocomp = GetComponent<InfoComponent>();
        if (infocomp != null) infocomp.Initialize();

        // 게임 시작 직후엔 반드시 Idle (IsWalking = false)
        if (animator != null)
            animator.SetBool(HashWalk, false);
    }

    void Start()
    {
        if (animator == null)
            Debug.LogWarning("[PlayerController] Animator 컴포넌트가 연결되지 않았습니다.");
    }

    void Update()
    {                
        if (!GameManager.Instance.IsPlayerTurn)
        {
            if (animator != null)
                animator.SetBool(HashWalk, false);
            return;
        }

        TouchCheck();
        // 클릭 입력 처리
        if (touch && GameManager.Instance.selectCard == null)
        {
            // 마우스 포인터 → 월드 좌표 변환 (Z값 지정!)
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            //z값 어짜피 무시됨

            // 경로 계산
            GetPath(worldPos);

            if (path.Count > 0)
            {
                Vector2Int nextNode = path.First.Value;
                target = new Vector3(nextNode.x, nextNode.y, 0f);

                Moved();
            }
            else
            {
                target = null;
            }
                touch = false;
        }
        bool shouldWalk = target.HasValue;
        if (animator != null)
            animator.SetBool(HashWalk, shouldWalk);

    }

    private void TouchCheck()
    {
        if (Input.touchCount != 1)
            return;

        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                if (IsPointerOverUIObject())
                {
                    isTouchMove = true;
                    return;
                }
                isTouchMove = false;
                break;
            case TouchPhase.Moved:
                isTouchMove = true;
                break;
            case TouchPhase.Ended:
                if (!isTouchMove)
                {
                    this.touch = true;
                }
                break;
        }
    }

    private bool IsPointerOverUIObject()
    {
        var touchPosition = Touchscreen.current.position.ReadValue();
        var eventData = new PointerEventData(EventSystem.current) { position = touchPosition };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    public override void NextStep()
    {
        if (path.Count > 0)
            path.RemoveFirst();

        if (path.Count > 0)
        {
            // 이동할 노드가 남아 있을 때
            Vector2Int next = path.First.Value;
            target = new Vector3(next.x, next.y, 0f);
        }
        else
        {
            // 더 이상 이동할 경로가 없을 때 → 이동 애니메이션 종료
            target = null;
            OnTurnEnd();
        }
    }

    public void OnTurnBegin()
    {
        // 턴이 시작될시 애니메이션이 꺼져 있도록
        if (animator != null)
            animator.SetBool(HashWalk, false);
        target = null;  // 잔여 경로가 남아있다면 바로 초기화
        path.Clear();
    }

    public void OnTurnEnd()
    {
        GameManager.Instance.EntityActionComplete(this);
    }

    public override Vector3? TargetPos => GameManager.Instance.IsPlayerTurn ? target : null;

}
