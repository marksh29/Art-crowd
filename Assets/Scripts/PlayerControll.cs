using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour
{
    public static PlayerControll Instance;
    [Header("--------Options--------")]
    [SerializeField] float moveSpeed;
    [SerializeField] int addMoney;

    [Header("--------Game--------")]
    [SerializeField] PathFollower path;
    [SerializeField] Text moneyText;
    int money;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        path.speed = moveSpeed;
        //money = PlayerPrefs.GetInt("money");
        moneyText.text = money.ToString();
    }
    void FixedUpdate()
    {
        if (Controll.Instance._state == "Game")
        {
                               
        }
    }
    public void AddMoney()
    {
        money += addMoney;
        moneyText.text = money.ToString();
        PlayerPrefs.SetInt("money", money);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
            Win();
        if (other.gameObject.tag == "Tutorial")
        {
            Tutorial.Instance.TutorialOn();
            other.gameObject.SetActive(false);
        }            
    }
    public void Lose()
    {
        Line.Instance.CleareLine();
        Controll.Instance.Set_state("End");
        Controll.Instance.Set_state("Lose");
        path.speed = 0;      
    }
    public void Win()
    {
        Line.Instance.CleareLine();
        Controll.Instance.Set_state("End");
        Line.Instance.StartGame("stay");       
        Gener.Instance.StartEnd(money);
        path.speed = 0;
    }
}
