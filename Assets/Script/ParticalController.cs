using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ParticalController : MonoBehaviour
{
    ShadowCaster2D shadowCaster;

    public Transform destination;
    GameObject player;
    Animator anim;
    Rigidbody2D playerRb;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager =  GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>(); 
        playerRb = player.GetComponent<Rigidbody2D>();

        shadowCaster = player.GetComponent <ShadowCaster2D>();
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
        audioManager.PlaySFX(audioManager.portalIn);
        playerRb.simulated = false;
        anim.SetBool("portalin",true);
        StartCoroutine(MoveInPortal());
        yield return new WaitForSeconds(0.5f);
        audioManager.PlaySFX(audioManager.portalOut);
        anim.SetBool("portalin", false);
        player.transform.position = destination.position;



        Physics2D.SyncTransforms();
        playerRb.linearVelocity = Vector2.zero; //Reset van toc
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
            yield return new WaitForEndOfFrame(); //cho het khung hinh hien tai
            timer += Time.deltaTime; //tang timer theo thoi gian troi qua
        }
    }



}
