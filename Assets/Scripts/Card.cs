using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] protected Image icon;
    [SerializeField] protected TextMeshProUGUI des;
    public CardSpawner cardSpawner;
    public Player player;
    private int id;
    private float value;

    public void SetInfo(CardSO cardSO)
    {
        icon.sprite = cardSO.icon;
        des.text = cardSO.des;
        id = cardSO.id;
        value = cardSO.value;
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        cardSpawner = FindObjectOfType<CardSpawner>();
    }
    public void DoCardFunc()
    {
        
        switch (id)
        {
            case 0:
                player.AddHp(value);
                break;
            case 1:
                player.AddDmg(value);
                break;
            case 2:
                player.AddSpeed(value);
                break;
            case 3:
                player.AddEXPgainRate(value);
                break;
            case 4:
                player.AddTurnRate(value);
                break;
            case 5:
                player.AddDef(value);
                break;
            case 6:
                cardSpawner.addNumberCard();
                break;
            case 7:
                cardSpawner.addMaxRefreshTime();
                break;
            case 8:
                player.AddRearAttack();
                cardSpawner.RemoveOneCard(id);
                break;
            case 9:
                player.AddDoubleAttack(value);
                cardSpawner.RemoveOneCard(id);

                break;
            default: break;
        }
        UIManager.Instance.HideCardPanel();
        GameManager.Instance.UnPauseGame();
    }
}
