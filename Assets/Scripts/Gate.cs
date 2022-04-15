using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] bool oneShoot;
    public int count;
    [SerializeField] GameObject gate;
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
}
