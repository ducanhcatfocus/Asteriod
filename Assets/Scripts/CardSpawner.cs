using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private List<CardSO> cardSOs;
    [SerializeField] private Transform parent;
    [SerializeField] private int numberCards = 3;
    [SerializeField] private int maxRefreshTime = 1;
    [SerializeField] private int refreshTime;
    public TextMeshProUGUI cardRefreshText;


    private void Start()
    {
        refreshTime = maxRefreshTime;
        cardRefreshText.text = "Refresh Times: " + refreshTime;
    }
    public void CreateCard()
    {

        List<int> indexs = new List<int>();
        for(int i = 0; i < cardSOs.Count; i++)
        {
            indexs.Add(i);
        }

        for(int i = 0; i < numberCards; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, indexs.Count);
            int selectedIndex = indexs[randomIndex];
            Card cardIns = Instantiate(cardPrefab, parent);
            cardIns.SetInfo(cardSOs[selectedIndex]);

            indexs.RemoveAt(randomIndex);
        }     
    }

    public void RefreshCard()
    {
        if(refreshTime > 0)
        {
            RemoveCard();
            CreateCard();
            refreshTime--;
            cardRefreshText.text = "Refresh Times: " + refreshTime;

        }

    }
    public void addNumberCard()
    {
        if(numberCards < 4)
        {
            numberCards++;
        }
    }

    public void addMaxRefreshTime()
    {
        if (maxRefreshTime < 3)
        {
            maxRefreshTime++;
            refreshTime++;
            cardRefreshText.text = "Refresh Times: " + refreshTime;

        }
    }

    public void RemoveCard()
    {
        foreach (Transform child in parent)
        {
           Destroy(child.gameObject);
        }
    }
}
