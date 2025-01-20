
using UnityEngine;

public class BackToMainMenu : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneController.Instance.BackToMainMenu();
        }
    }

}
