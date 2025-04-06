using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource mainMusicSource;
    public AudioSource gameMusicSource;

    public AudioSource correctSource;
    public AudioSource wrongSource;
    public AudioSource endSource;
 
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
        endSource.mute = !mainMusicSource.mute;
        mainMusicSource.mute = !mainMusicSource.mute;

    }
    public void PlayMainMusic()
    {
        mainMusicSource.Play();
    }
    public void StopMainMusic()
    {
        mainMusicSource.Stop();
    }
    public void PlayGameMusic()
    {
        gameMusicSource.Play();
    }
    public void StopGameMusic()
    {
        gameMusicSource.Stop();
        
    }
    public void CorrectCoinAudioClip()
    {
        correctSource.Play();

    }
    public void WrongAudioClip()
    {
        wrongSource.Play();

    }
      public void EndAudioClip()
    {
        endSource.Play();

    }


}
