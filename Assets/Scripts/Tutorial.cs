using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance;
    [SerializeField] float slowSpeed, normalSpeed;
    [SerializeField] int stageID;
    [SerializeField] GameObject tutorPanel;
    [SerializeField] GameObject[] stageObj, swipeObj;
    [SerializeField] string[] stageText;
    [SerializeField] Text stageTxt;
    [SerializeField] PathFollower path;
    bool on;
       
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (Instance == null) Instance = this;
        if (PlayerPrefs.GetInt("Tutorial") == 1)
            Application.LoadLevel(PlayerPrefs.GetInt("curLevel"));
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void TutorialOn()
    {
        on = true;
        tutorPanel.SetActive(true);
        stageObj[stageID].SetActive(true);
        //swipeObj[stageID].SetActive(true);
        stageTxt.text = stageText[stageID];
        path.speed = slowSpeed;
    }
    public void TutorialOff()
    {
        if(on)
        {
            stageObj[stageID].SetActive(false);
            //swipeObj[stageID].SetActive(false);
            stageID++;
            path.speed = normalSpeed;
            tutorPanel.SetActive(false);
            on = false;
        }               
    }
}
