using UnityEngine;

public class MoveToCenter : MonoBehaviour
{
    [SerializeField] Transform targetPosition;
    [SerializeField] float duration;
    [SerializeField] LeanTweenType easeType;
    private void Start()
    {
        Move();
    }

    void Move()
    {
        LeanTween.moveX(gameObject, targetPosition.position.x, duration).setEase(easeType);
    }
}
