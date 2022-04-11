using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [Header("--------Options--------")]
    [SerializeField] float moveSpeed;
    [SerializeField] float boostSpeed, slowSpeed, boostTime;
    [Header("----------------")]
    [SerializeField] float moveRightSpeed;
    [SerializeField] float limitXX;
    [Header("---------------")]
    public float addAngle;
    public float changeAngle;
    [Header("--------GamePlay--------")]
    [SerializeField] Transform head;
    [SerializeField] float curSpeed;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        curSpeed = moveSpeed;        
    }
    void FixedUpdate()
    {
        if(Controll.Instance._state == "Game")
        {
            transform.Translate(Vector3.forward * curSpeed * Time.deltaTime);
           
            if (Input.GetMouseButtonDown(0))
            {
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            if (Input.GetMouseButton(0))
            {
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                //if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) // swip up
                //if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) // swip down
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) // swip left
                {
                    if (head.transform.position.x > -limitXX)
                        head.transform.Translate(-Vector3.right * moveRightSpeed * Time.deltaTime);
                }
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) // swip right
                {
                    if (head.transform.position.x < limitXX)
                        head.transform.Translate(Vector3.right * moveRightSpeed * Time.deltaTime);
                }
            }

        }
    } 
    public void Boost()
    {
        curSpeed = boostSpeed;
        StartCoroutine(SlowSpeed());
    }
    IEnumerator SlowSpeed()
    {
        yield return new WaitForSeconds(boostTime);
        while(curSpeed > moveSpeed)
        {
            curSpeed -= slowSpeed;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
