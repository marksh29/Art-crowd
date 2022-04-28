using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] bool oneShoot;
    public int count, skinID, moneyCount;
    [SerializeField] GameObject gate, moneyPrefab;

    void Start()
    {
    }
    void Update()
    {
              
    }  
    public int SetGate()
    {
        if (oneShoot)
            gate.SetActive(false);      
        return count;
    }
    public int SetSkinGate()
    {
        if (oneShoot)
            gate.SetActive(false);
        return skinID;
    }
    public int SetMoneyGate()
    {
        if (oneShoot)
            gate.SetActive(false);
        moneyPrefab.SetActive(true);
        return moneyCount;
    }
}
