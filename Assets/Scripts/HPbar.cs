using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    // Start is called before the first frame update
    public Image hpbar;
    public Image expbar;
    public TextMeshProUGUI displayTimeText;
    public TextMeshProUGUI displayLvText;

    int time;
    int minute;
    int second;


    private void Update()
    {
        UpdateTime();
    }

    void UpdateTime()
    {
        time = (int)GameManager.Instance.GetTime();
        minute = time / 60;
        second = time % 60;
        if (second < 10)
        {
            displayTimeText.text = minute + ":" + "0" + second;

        }
        else
        {
            displayTimeText.text = minute + ":" + second;

        }
    }

    public void DisplayLv(int lv)
    {
        displayLvText.text = "Lv" + " " + lv.ToString();
    }

    public void UpdateHP(float percent)
    {
        hpbar.fillAmount = percent;
    }

    public void UpdateExp(float percent)
    {
        expbar.fillAmount = percent;
    }
}
