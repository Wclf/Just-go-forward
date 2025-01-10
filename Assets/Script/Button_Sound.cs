using UnityEngine;
using UnityEngine.UI;

public class Button_Sound : MonoBehaviour
{
    AudioManager audioManager;
    Button button;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        button = gameObject.GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        audioManager.PlaySFX(audioManager.ButtonClick);
    }


}
