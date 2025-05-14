using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Movement : MonoBehaviour
{
    public void MoveTo(Vector2 targetPos)
    {
        transform.position = targetPos;
    }
}
