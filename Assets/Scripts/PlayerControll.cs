using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class PlayerControll : MonoBehaviour
{
    [Header("--------Options--------")]
    [SerializeField] float moveSpeed;
    public float boostSpeed, slowSpeed, boostTime, speedRight, emotionTime, addScaleSpeed, addShapeSpeed;
    public float headForwardRoatete, glassDestroy, moveLimit;

    [Header("--------Game--------")]
    [SerializeField] PathFollower path;
    [SerializeField] float speed; 
    [SerializeField] GameObject head;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    void Start()
    {
        moveLimit = moveLimit == 0 ? 3.5f : moveLimit;
        path.speed = moveSpeed;
    }
    void FixedUpdate()
    {
        if (Controll.Instance._state == "Game")
        {
            head.transform.Rotate(Vector3.right * headForwardRoatete);

            if (Input.GetMouseButtonDown(0))
            {
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            if (Input.GetMouseButton(0))
            {
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) // swip left
                {
                    //head.GetComponent<Rigidbody>().velocity = new Vector3(0, head.GetComponent<Rigidbody>().velocity.y, 0);
                    if(transform.GetChild(0).transform.localPosition.x > -moveLimit)
                        transform.GetChild(0).transform.Translate(-Vector3.right * speedRight * Time.deltaTime);
                }
                else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) // swip right
                {
                    //head.GetComponent<Rigidbody>().velocity = new Vector3(0, head.GetComponent<Rigidbody>().velocity.y, 0);
                    if (transform.GetChild(0).transform.localPosition.x < moveLimit)
                        transform.GetChild(0).transform.Translate(Vector3.right * speedRight * Time.deltaTime);
                }                    
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }            
        }
    }  

    public void Boost()
    {
        path.speed = boostSpeed;
        StartCoroutine(SlowSpeed());
    }  
    IEnumerator SlowSpeed()
    {
        yield return new WaitForSeconds(boostTime);
        while (path.speed > moveSpeed)
        {
            path.speed -= slowSpeed;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void Lose()
    {
        Controll.Instance.Set_state("Lose");
        path.speed = 0;
        headForwardRoatete = 0;
        head = null;        
    }
    public void Win()
    {
        Controll.Instance.Set_state("Win");
        path.speed = 0;
        headForwardRoatete = 0;
        head = null;
    }
}
