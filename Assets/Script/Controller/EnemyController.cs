using UnityEngine;

public class EnemyController : Controller
{
    public override bool IsActive => !GameManager.Instance.isPlayerTurn;

    static int count = 0;

    void Update()
    {
        if (IsActive)
        {
            GetPath(transform.position + new Vector3(Random.Range(-1,2), Random.Range(-1, 2)));
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.EnemyTurnEvent += StartTrun;
    }

    private void OnDisable()
    {
        GameManager.Instance.EnemyTurnEvent -= StartTrun;

    }

    private void StartTrun()
    {
        count++;
    }


    public override void Next()
    {
        path.RemoveAt(0);

        count--;
        if (count <= 0)
        {
            GameManager.Instance.EndTurn();
        }
    }
}
