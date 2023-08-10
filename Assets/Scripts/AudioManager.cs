using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   
    private static AudioManager _instance;

    public AudioSource AudioSource;
    public AudioClip shotClip;
    public AudioClip takeDmgClip;

    public static AudioManager Instance { get { return _instance; } }

   

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShotSound()
    {
        AudioSource.PlayOneShot(shotClip);
    }
    public void playTakeDmg()
    {
        AudioSource.PlayOneShot(takeDmgClip);
    }

}
