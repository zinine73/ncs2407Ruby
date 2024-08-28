using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject worldDialogBox;
    public GameObject UIDialogBox;
    public TMP_Text talkText;
    public TMP_Text talkUIText;
    public GameObject stickUI;
    public GameObject bulletUI;
    public GameObject talkUI;
    float timerDisplay;
     
    // Start is called before the first frame update
    void Start()
    {
        worldDialogBox.SetActive(false);
        UIDialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
#if (!UNITY_ANDROID)
                worldDialogBox.SetActive(false);
#else
                UIDialogBox.SetActive(false);
                UIOnOff(true);
#endif
            }
        }
    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
#if (!UNITY_ANDROID)
        worldDialogBox.SetActive(true);
#else
        UIDialogBox.SetActive(true);
        UIOnOff(false);
#endif
    }

    public void ChangeDialogValue()
    {
        talkText.text = talkUIText.text = "Thanks! Good Job!";
    }

    public void UIOnOff(bool value)
    {
        stickUI.SetActive(value);
        bulletUI.SetActive(value);
        talkUI.SetActive(value);
    }
}
