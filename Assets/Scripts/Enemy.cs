using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Enemy : MonoBehaviour
{
    [SerializeField] TextMeshPro txt;
    [SerializeField] int count;
    void Start()
    {
        count = transform.childCount;
        SetCount();
    }
    public void RemoveCount()
    {
        count--;
        if (count <= 0)
            gameObject.SetActive(false);
    }
    public void SetCount()
    {
        int ct = 0;
        for (int i = 1; i < transform.childCount; i++)
        {
            ct += transform.GetChild(i).GetComponent<Enem>().curLife > 0 ? transform.GetChild(i).GetComponent<Enem>().curLife : 0;
        }
        txt.text = ct.ToString();
    }
}
