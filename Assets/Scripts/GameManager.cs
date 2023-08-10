using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private float currentTime;
    private bool isPause;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.timeSinceLevelLoad;
    }
    
    public void ResetTime()
    {

    }

    public float GetTime()
    {
        return currentTime;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        //AudioManager.instance.MuteAudio();
        isPause = true;
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1;
        //AudioManager.instance.MuteAudio();
        UIManager.Instance.HidePausePanel();
        isPause = false;
    }

    public bool GetPauseState()
    {
        return isPause;
    }

    public void SetGameOver()
    {
        PauseGame();
        UIManager.Instance.DisplayLoseGamePanel();
    }

   

}
