using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject worldDialogBox;
    public GameObject UIDialogBox;
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
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
                worldDialogBox.SetActive(false);
#else
                UIDialogBox.SetActive(false);
#endif
            }
        }
    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
        worldDialogBox.SetActive(true);
#else
        UIDialogBox.SetActive(true);
#endif
    }
}
