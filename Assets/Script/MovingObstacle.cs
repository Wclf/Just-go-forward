using System.Collections;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Range(0f, 5f)]
    public float speed;
    
    [Range(0f, 2f)]
    public float timeWait;
    
    int speedMultiplier = 1;

    Vector3 targetPos;

    public GameObject ways;
    public Transform[] wayPoints;
    int pointIndex;
    int pointCount;

    int direction = 1;

    private void Awake()
    {
        wayPoints = new Transform[ways.transform.childCount];
        for(int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        pointCount = wayPoints.Length;
        pointIndex = 1;
        targetPos = wayPoints[pointIndex].transform.position;
    }

    private void Update()
    {
        var step = speedMultiplier*  speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        if(transform.position == targetPos )
        {
            
            NextPoint();
        }
    }

    void NextPoint()
    {
        if(pointIndex == pointCount - 1)
        {
            direction = -1;
        }
        if(pointIndex == 0)
        {
            direction = 1;
        }
        pointIndex += direction;
        targetPos = wayPoints[pointIndex].transform.position;
        StartCoroutine(waitTime());
    }

    IEnumerator waitTime()
    {
        speedMultiplier = 0;
        yield return new WaitForSeconds(timeWait);
        speedMultiplier = 1;
    }
}
