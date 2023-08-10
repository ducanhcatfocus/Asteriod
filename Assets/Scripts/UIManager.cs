using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    public GameObject cardPanel;
    public GameObject pausePanel;
    public GameObject losegamePanel;



    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
         
        }
    }

    public void DisplayCardPanel()
    {
        cardPanel.SetActive(true);
        CardSpawner cardSpawner = cardPanel.GetComponent<CardSpawner>();
        cardSpawner.CreateCard();
    }
    public void HideCardPanel()
    {
        cardPanel.SetActive(false);
        CardSpawner cardSpawner = cardPanel.GetComponent<CardSpawner>();
        cardSpawner.RemoveCard();
    }
    public void DisplayPausePanel()
    {
        pausePanel.SetActive(true);
        
    }
    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
        
    }

    public void DisplayLoseGamePanel()
    {
        losegamePanel.SetActive(true);
  
        
    }

}
