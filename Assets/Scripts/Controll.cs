
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using PathCreation.Examples;

public class Controll : MonoBehaviour
{
    public static Controll Instance;
    public string _state;
    [SerializeField] GameObject[] panels;
    [SerializeField] GameObject confeti, loseConfeti;
    [SerializeField] Text leveltext;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        PlayerPrefs.SetInt("curLevel", Application.loadedLevel);
        if(Application.loadedLevel == 1)
            PlayerPrefs.SetInt("Tutorial", 1);
        GameAnalityc.Instance.Start_level((Application.loadedLevel + 1));
    }
    void Start()
    {
        leveltext.text = "LEVEL " + (PlayerPrefs.GetInt("level") + 1).ToString();
        PathPlacer.Instance.Off();
        //Set_state("Game");
    }
  
    public void Set_state(string name)
    {
        _state = name;
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(panels[i].name == name ? true : false);
        } 
        
        switch(_state)
        {          
            case ("Win"):
                GameAnalityc.Instance.Win_level((Application.loadedLevel + 1));
                confeti.SetActive(true);                
                Line.Instance.End(false);                              
                break;
            case ("Lose"):
                GameAnalityc.Instance.Lose_level((Application.loadedLevel + 1));
                loseConfeti.SetActive(true);
                Line.Instance.End(false);
                break;
        }
    } 
    public void StartLevel()
    {
        Line.Instance.StartGame("move");
        //Line.Instance.End(true);
        Set_state("Game");        
    }
    public void Restart()
    {
        SceneManager.LoadScene(Application.loadedLevel);
    }
    public void Next_level()
    {
        if (Application.loadedLevel != 0)
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        SceneManager.LoadScene(Application.loadedLevel == Application.levelCount -1 ? 1 : (Application.loadedLevel + 1));
    }   
}
