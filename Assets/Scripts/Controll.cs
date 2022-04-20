
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
                confeti.SetActive(true);                
                Line.Instance.End(false);
                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
                break;
            case ("Lose"):
                loseConfeti.SetActive(true);
                Line.Instance.End(false);
                break;
        }
    } 
    public void StartLevel()
    {
        Line.Instance.StartGame("move");
        Line.Instance.End(true);
        Set_state("Game");        
    }
    public void Restart()
    {
        SceneManager.LoadScene(Application.loadedLevel);
    }
    public void Next_level()
    {
        SceneManager.LoadScene(Application.loadedLevel == Application.levelCount -1 ? 0 : (Application.loadedLevel + 1));
    }
    
    public IEnumerator Win()
    {
        yield return new WaitForSeconds(2);
        Set_state("Win");        
    }  

    public IEnumerator Lose()
    {
        yield return new WaitForSeconds(1);
        Set_state("Lose");
    }  
}
