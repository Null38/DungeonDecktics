using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Movement : MonoBehaviour
{
    private Controller controller;

    private void Start()
    {
        controller = GetComponent<Controller>();
    }

    
    void FixedUpdate()
    {
        if (true)//고쳐야함
        {
            Move();
        }
    }


    public void Move()
    {
        if (controller.TargetPos == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, (Vector3)controller.TargetPos, DataManager.Speed * Time.deltaTime);

        if (transform.position == (Vector3)controller.TargetPos)
        {
            controller.NextStep();
        }
    }
}
