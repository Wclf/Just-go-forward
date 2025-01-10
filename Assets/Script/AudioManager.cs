using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------------Audio Source-------------")]
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------------Audio Clip---------------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip wallTouch;
    public AudioClip checkPoint;
    public AudioClip portalIn;
    public AudioClip portalOut;
    public AudioClip ButtonClick;

    private void Start()
    {
        MusicSource.clip = background;
        MusicSource.Play(); 
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
