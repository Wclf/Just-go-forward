using System.Collections;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameController : MonoBehaviour
{
    Vector2 checkPointPos;
    Rigidbody2D rb;
    ShadowCaster2D shadowCaster;
    CameraFollow cameraFollow;
    AudioManager audioManager;

    [SerializeField] ParticleSystem diePartical;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        rb = GetComponent<Rigidbody2D>();
        shadowCaster = GetComponent<ShadowCaster2D>();

    }

    private void Start()
    {
        checkPointPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle")) //so sanh khi cham vat the co gan tag obstacle goi ham die
        {
            Die();
        }
    }

    void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }

    //ham updatecheckpoint cho script CheckPoint khi cham vao vat the co tag Player
    public void UpdateCheckPoint(Vector2 pos)
    {
        checkPointPos = pos;
    }

    IEnumerator Respawn(float duration)
    {
        audioManager.PlaySFX(audioManager.death);
        GetComponent<Animator>().enabled = false; // tat animator de tranh cac hieu ung cu con chay
        cameraFollow.anim.SetBool("isFlash", true); //bat hieu ung flash cua man hinh
        diePartical.Play(); //bat hieu ung khi chet
        rb.simulated = false; //tam thoi tat vat ly cho player
        rb.linearVelocity = Vector2.zero; //dung chuyen dong cho player de sau khi chet se set ve van toc ban dau
        shadowCaster.enabled = false; //tat shadow caster khi chet tranh khi chet bong van con
        transform.localScale = Vector3.zero; //thu nho player ve kich thuoc 0
        yield return new WaitForSeconds(duration);
        rb.simulated = true;
        GetComponent<Animator>().enabled = true;
        transform.position = checkPointPos;
        transform.localScale = Vector3.one;

        shadowCaster.enabled = true;
        cameraFollow.anim.SetBool("isFlash", false);
    }
}
