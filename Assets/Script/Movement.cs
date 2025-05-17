using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Movement : MonoBehaviour
{
    private Controller controller;

    private void Start()
    {
        controller = GetComponent<Controller>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (controller.IsActive)
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
            controller.Next();
        }
    }
}
