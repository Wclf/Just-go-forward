using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Animator transitionAnim;
    private static SceneController instance;
    public static SceneController Instance { get => instance;} // vi du ve single pattern khong cho sceneController xuat hien lan thu 2

    private void Awake()
    {
        if(instance == null)
        {
            //khong xoa gameobject chua script khi load scene moi
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //neu scene moi cx co gameobject nay thi xoa pha huy gameobject hien tai;
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); // load next level
        transitionAnim.SetTrigger("Start");

    }




}
