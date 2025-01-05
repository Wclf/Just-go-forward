using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform posA, posB;
    public float speed;
    Vector3 targetPos;

    Movement movement;
    Rigidbody2D rb;
    Vector3 moveDirection;

    //khai bao rb cho nhan vat tim thong qua findobject de set trong luc luc tren be mat platform
    Rigidbody2D playerRb;

    //nang cao dua nhieu vi tri cho platform
    public GameObject ways;
    public Transform[] wayPoints;
    int pointIndex;
    int pointCount;
    int direction = 1;

    public float WaitDuration;

    private void Awake()
    {
       movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        wayPoints = new Transform[ways.transform.childCount]; // lay cac tranform trong object ways ket qua ra: wayPoints[3]) 
        for(int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            //gan tung ways vao list wayPoints;
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        pointIndex = 1; //set pointindex co gia tri la 1
        pointCount = wayPoints.Length; //set pointcound co gai tri bang do dai cua wayPoints = 3
        targetPos = wayPoints[1].transform.position; //set targetpos tai vi tri cua tranform cua wayPoint dau tien
        DirectionCalculate(); //tinh toan doi huong

    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, targetPos) < 0.05f)
        {
            NextPoint();
        }

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }

    void NextPoint()
    {
        transform.position = targetPos; //dat vi tri hien tai la targetpos
        moveDirection = Vector3.zero;
        if(pointIndex == pointCount - 1) //nen tang dat den diem cuoi cung
        {
            direction = -1; //doi huong
        }
        if(pointIndex == 0)
        {
            direction = 1; //va nguoc lai
        }
        pointIndex += direction; //cap nhat chi so diem tiep theo
        targetPos = wayPoints[pointIndex].transform.position; //cap nhat lai targetpos cho diem tiep theo
        StartCoroutine(WaitNextPoint());

    }

    IEnumerator WaitNextPoint()
    {
        yield return new WaitForSeconds(WaitDuration);
        DirectionCalculate();
    }

    void DirectionCalculate()
    {
        //tinh toan di chuyen chuan hoa vector dua ve (1,0,0) hoac (-1,0,0) nen tang se di chuyen theo huong truc (+x) hoac (-x)
        moveDirection = (targetPos - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            movement.isOnPlatform = true; //dieu kien ben scipt movement thanh true
            movement.platformRb = rb; 
            playerRb.gravityScale = playerRb.gravityScale * 50; //tang trong luc cho nhan vat de khi dung tren platform ko bi day len

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerRb.linearVelocity = Vector2.zero;
            movement.isOnPlatform = false;
            playerRb.gravityScale = playerRb.gravityScale / 50; //tra lai trong luc ban dau


        }
    }

}
