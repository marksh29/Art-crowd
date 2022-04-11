using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class PlayerControll : MonoBehaviour
{
    [Header("--------Options--------")]
    [SerializeField] float moveSpeed;
   
    [Header("--------Game--------")]
    [SerializeField] PathFollower path;
    [SerializeField] float speed; 
    [SerializeField] GameObject head;

    void Start()
    {
        path.speed = moveSpeed;
    }
    void FixedUpdate()
    {
        if (Controll.Instance._state == "Game")
        {
                               
        }
    }  
       
    public void Lose()
    {
        Controll.Instance.Set_state("Lose");
        path.speed = 0;
        head = null;        
    }
    public void Win()
    {
        Controll.Instance.Set_state("Win");
        path.speed = 0;
        head = null;
    }
}
