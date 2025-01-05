using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    [SerializeField] Transform targetPosition;
    [SerializeField] float duration;
    [SerializeField] LeanTweenType easeType;


    [SerializeField] GameObject pauseMenu;
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        LeanTween.moveY(PausePanel.gameObject, targetPosition.position.y, duration).setEase(easeType).setIgnoreTimeScale(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        LeanTween.moveY(PausePanel.gameObject, Screen.height*2, duration)
            .setEase(easeType)
            .setIgnoreTimeScale(true)
            .setOnComplete(() =>
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            });
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

    }

}
