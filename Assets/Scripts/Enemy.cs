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
        count = transform.childCount - 1;
        txt.text = count.ToString();
    }
    public void RemoveCount(GameObject obj)
    {
        Destroy(obj);
        count--;
        txt.text = count.ToString();
        if (count <= 0)
            gameObject.SetActive(false);
    }  
}
