using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ParticalController : MonoBehaviour
{
    public Transform destination;
    GameObject player;
    Animator anim;
    Rigidbody2D playerRb;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>(); 
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(Vector2.Distance(player.transform.position, transform.position) > 0.3f)
            {
                StartCoroutine(PortalIn());
            }
        }
    }

    IEnumerator PortalIn()
    {
        playerRb.simulated = false;
        anim.SetBool("portalin",true);
        StartCoroutine(MoveInPortal());
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("portalin", false);
        player.transform.position = destination.position;
        playerRb.linearVelocity = Vector2.zero;
        anim.SetBool("portalout",true);
        yield return new WaitForSeconds(0.5f);
        playerRb.simulated = true;
        anim.SetBool("portalout", false);
    }

    IEnumerator MoveInPortal()
    {
        float timer = 0;
        while(timer < 0.5f) 
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, 3 * Time.deltaTime); //dua player ve gan voi tam cua cong dich chuyen
            yield return new WaitForEndOfFrame(); //che het khung hinh hien tai
            timer += Time.deltaTime; //tang timer theo thoi gian troi qua
        }
    }



}
