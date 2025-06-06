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
    private static readonly int HashHit = Animator.StringToHash("Hit");
    private static readonly int HashDie = Animator.StringToHash("Die");
    private static readonly int HashAttack = Animator.StringToHash("Attack");

    private Vector3? target = null; 

    private bool touch = false;
    private bool isTouchMove = true;

    void Awake()
    {
        DataManager.player = this;

        info.SetInfo();
        info.dumb();
        info.Initialize();
        info.dumbEq();

        InGameUIManager.GetRestEvent += GetRest;

        // 게임 시작 직후엔 반드시 Idle (IsWalking = false)
        if (animator != null)
            animator.SetBool(HashWalk, false);
    }
    public void CancelMove()
    {
        path.Clear();
        target = null;
        if (animator != null)
            animator.SetBool(HashWalk, false);
    }

    void GetRest()
    {
        info.InitCost();
        Debug.LogWarning("카드 드로우 시키기");

        GameManager.Instance.cardPile.DrawToHand(5);
        OnTurnEnd();
    }
    public void PlayAttackAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger(HashAttack);
        }
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
            target = new Vector3(path.First.Value.x, path.First.Value.y, 0f);
        else
            target = null;

        OnTurnEnd();
    }

    public void OnTurnBegin()
    {
        info.currentShield = 0;
        // 턴이 시작될시 애니메이션이 꺼져 있도록
        if (animator != null)
            animator.SetBool(HashWalk, false);
    }

    public void OnTurnEnd()
    {
        GameManager.Instance.EntityActionComplete(this);
    }

    public override Vector3? TargetPos => GameManager.Instance.IsPlayerTurn ? target : null;

}
