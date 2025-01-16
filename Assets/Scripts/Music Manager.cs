using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource mainMusicSource;

    public AudioSource correctSource;
    public AudioSource wrongSource;
 
    public GameObject muteUI;
    public GameObject unmuteUI;
     
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mainMusicSource.Play();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void MuteAndUnmuteAllMusic()
    {
        muteUI.SetActive(!mainMusicSource.mute);
        unmuteUI.SetActive(mainMusicSource.mute);
        correctSource.mute = !mainMusicSource.mute;
        wrongSource.mute = !mainMusicSource.mute;
        mainMusicSource.mute = !mainMusicSource.mute;

    }
    public void CorrextCoinAudioClip()
    {
        correctSource.Play();

    }
    public void WrongAudioClip()
    {
        wrongSource.Play();

    }


}
