using System.Collections;
using System.Xml.Serialization;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameController : MonoBehaviour
{
    Vector2 StartPos;
    Rigidbody2D rb;
    ShadowCaster2D shadowCaster;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shadowCaster = GetComponent<ShadowCaster2D>();
    }

    private void Start()
    {
        StartPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        rb.simulated = false;
        rb.linearVelocity = Vector2.zero;
        shadowCaster.enabled = false;
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(duration);
        rb.simulated = true;

        transform.position = StartPos;
        transform.localScale = Vector3.one;

        shadowCaster.enabled = true;


    }
}
