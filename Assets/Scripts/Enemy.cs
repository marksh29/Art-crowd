using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Enemy : MonoBehaviour
{
    [SerializeField] TextMeshPro txt;
    [SerializeField] int count;
    [SerializeField] bool massCounter;
    void Start()
    {
        count = transform.childCount -1;
        SetCount();
        txt.transform.parent.gameObject.SetActive(massCounter);
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Enem>().CounterOff(!massCounter);
        }
    }
    public void RemoveCount()
    {
        count--;
        if (count <= 0)
        {
            StartCoroutine(Off());           
        }            
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
    IEnumerator Off()
    {
        txt.transform.parent.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
