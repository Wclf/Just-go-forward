using UnityEngine;

public class MoveToTargetPosition : MonoBehaviour
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
        LeanTween.move(gameObject, targetPosition.position, duration).setEase(easeType);
    }
}
