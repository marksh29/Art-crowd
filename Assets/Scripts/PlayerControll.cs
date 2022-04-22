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
    }
    public void Lose()
    {
        Controll.Instance.Set_state("Lose");
        path.speed = 0;      
    }
    public void Win()
    {
        Line.Instance.StartGame("stay");
        Controll.Instance.Set_state("End");
        Gener.Instance.StartEnd(money - 1);
        path.speed = 0;
    }
}
