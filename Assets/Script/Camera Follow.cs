using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    Vector3 velocity = Vector3.zero;

    [Range(0f, 1f)]
    public float smoothTime;
    public Vector3 positionOffset; // set trục z cho cam ra xa



    [Header("Limit X , Y")]
    public Vector2 Xlimit;
    public Vector2 Ylimit;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + positionOffset;
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, Xlimit.x, Xlimit.y), Mathf.Clamp(targetPosition.y, Ylimit.x, Ylimit.y), -10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

}
