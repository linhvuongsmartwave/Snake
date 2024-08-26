using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    //public AudioClip bg;
    public AudioClip win;
    public AudioClip shoot;
    public AudioClip bought;
    //public AudioClip pointTouch;
    public AudioClip buttonClick;
    public AudioClip open;
    public AudioClip hit;
    public AudioClip coin;
    public AudioSource audioSource;


    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        //Background();
    }
    
    public void SetActive(bool isActive)
    {
        if (isActive) audioSource.volume = 1f;
        else audioSource.volume = 0f;
    }

    //public void Background()
    //{
    //    audioSource.clip = bg;
    //    audioSource.Play();
    //}

    public void StopAudio()
    {
        audioSource.Stop();
    }

    public void AudioButtonClick()
    {
        audioSource.PlayOneShot(buttonClick);
    }

    public void AudioWin()
    {
        audioSource.PlayOneShot(win);
    }

    //public void AudioPointTouch()
    //{
    //    audioSource.PlayOneShot(pointTouch);
    //}

    public void AudioBought()
    {
        audioSource.PlayOneShot(bought);
    }
    public void AudioShoot()
    {
        audioSource.PlayOneShot(shoot);
    }
    public void AudioOpen()
    {
        audioSource.PlayOneShot(open);
    }
    public void AudioHit()
    {
        audioSource.PlayOneShot(hit);
    }
    public void AudioCoin()
    {
        audioSource.PlayOneShot(coin);
    }

}
