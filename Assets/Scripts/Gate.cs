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
            gameObject.SetActive(false);      
        return count;
    }
    public int SetSkinGate()
    {
        if (oneShoot)
            gameObject.SetActive(false);
        return skinID;
    }
    public int SetMoneyGate()
    {
        if (oneShoot)
            gameObject.SetActive(false);
        moneyPrefab.SetActive(true);
        moneyPrefab.transform.parent = null;
        Destroy(moneyPrefab, 1);
        return moneyCount;
    }
}
