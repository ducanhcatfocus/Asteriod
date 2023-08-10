using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILose : MonoBehaviour
{
    // Start is called before the first frame update

    TextMeshProUGUI timePlayText;
    
    void Start()
    {
        timePlayText = GetComponent<TextMeshProUGUI>();
        DisplayTimePlay();
           
    }

    private void DisplayTimePlay()
    {
        int time = (int)GameManager.Instance.GetTime();
        int minute = time / 60;
        int second = time % 60;
        if (second < 10)
        {
            timePlayText.text ="Time play: " + minute + ":" + "0" + second;

        }
        else
        {
            timePlayText.text ="Time play: " + minute + ":" + second;

        }
    }
}
