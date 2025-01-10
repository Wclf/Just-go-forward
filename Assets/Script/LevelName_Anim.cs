using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelName_Anim : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelName;

    private void Awake()
    {
        levelName = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        levelName.text = "Level " + SceneManager.GetActiveScene().buildIndex;
    }

}
