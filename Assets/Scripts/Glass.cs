using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] GameObject effect;

    void Start()
    {
        
    }
    public void EffectOn()
    {
        effect.SetActive(true);
        gameObject.SetActive(false);
        //GetComponent<BoxCollider>().enabled = false;        
        //GetComponent<MeshRenderer>().enabled = false;
    }
}
