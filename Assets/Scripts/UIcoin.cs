using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcoin : MonoBehaviour
{
    public static UIcoin Instance;
    [SerializeField] Transform[] moveCoins;
    [SerializeField] Transform targetPos;
    void Start()
    {
        if (Instance == null)
            Instance = this;
    }
    void Update()
    {
        
    }
    public void StartMove(Transform pos)
    {

    }
}
