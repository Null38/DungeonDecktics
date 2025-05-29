using UnityEngine;

public class Selector : MonoBehaviour
{
    public RectTransform target;
    public float speed;
    public Vector2 padding;

    public Vector3 initialPosition;
    public Vector2 initialSize;

    void FixedUpdate()
    {
        if (target != null)
        {
            MoveToTarget();
        }
        else
        {
            MoveToInitialPosition();
        }
    }

    void MoveToTarget()
    {
        Vector3 targetPos = target.position;
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.fixedDeltaTime);

        RectTransform rectTransform = transform as RectTransform;
        Vector2 targetSizeWithPadding = (target.sizeDelta / 2) + padding;
        rectTransform.sizeDelta = Vector2.Lerp(rectTransform.sizeDelta, targetSizeWithPadding, speed * Time.fixedDeltaTime);
    }
    void MoveToInitialPosition()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, speed * Time.fixedDeltaTime);

        RectTransform rectTransform = transform as RectTransform;
        rectTransform.sizeDelta = Vector2.Lerp(rectTransform.sizeDelta, initialSize, speed * Time.fixedDeltaTime);
    }

    public void Select(RectTransform target)
    {
        this.target = target;
    }
}