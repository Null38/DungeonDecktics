using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;
using System;

public class PlayerController : Controller, ITurnBased
{
    public static event Action Moved;

    private Vector3? target = null;


    private bool touch = false;
    private bool isTouchMove = true;

    void Awake()
    {
        DataManager.player = this;
        var infocomp = GetComponent<InfoComponent>();
        if (infocomp != null) infocomp.Initialize();
    }

    void Start()
    {
    }

    void Update()
    {

        if (!GameManager.Instance.IsPlayerTurn)
            return;

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
                // LinkedList<Vector2Int> → Vector3 변환
                Vector2Int nextNode = path.First.Value;
                target = new Vector3(nextNode.x, nextNode.y, 0f);
                Moved();
            }
            touch = false;
        }
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
        
    }

    public void OnTurnEnd()
    {
        GameManager.Instance.EntityActionComplete(this);
    }

    public override Vector3? TargetPos => GameManager.Instance.IsPlayerTurn ? target : null;

}
