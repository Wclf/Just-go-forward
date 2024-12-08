using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameController gameController;
    SpriteRenderer spriteRenderer;
    Collider2D col;
    public Sprite passive, active;
    public Transform respawnPos;
    private void Awake()
    {
            col = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>(); //tim gamecontroller tu player (de goi ham UpdateCheckPoint)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameController.UpdateCheckPoint(respawnPos.position); //cap nhat vi tri checkpoint trong gamecontroller
            spriteRenderer.sprite = active; //thay doi sprite thanh active
            col.enabled = false; //tat colider cua checkpoint de tranh kich hoat lai
        }
    }
}
