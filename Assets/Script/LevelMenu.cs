
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButtons;
    public GameObject LevelPages;


    private void Awake()
    {
        ButtonsToArray();
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for(int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }
    
    public void OpenLevel(int numLevel)
    {
        string sceneName = "Man" + numLevel;
        SceneManager.LoadScene(sceneName);
    }

    void ButtonsToArray()
    {
        int childCountPages = LevelPages.transform.childCount;
        int childCount = levelButtons.transform.childCount;

        buttons = new Button[childCount * childCountPages];

        for (int i = 0; i < childCountPages; i++)
        {
            for(int j = 0; j < childCount; j++)
            {
                buttons[i * childCount + j] = LevelPages.transform.GetChild(i).transform.GetChild(j).GetComponent<Button>();
            }
        }

        //for(int i = 0; i < childCount;i++)
        //{
        //    buttons[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        //}
    }
}
