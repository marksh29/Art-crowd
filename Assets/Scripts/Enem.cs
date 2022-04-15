using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enem : MonoBehaviour
{
    public int life;
    int curLife;
    public GameObject deadEffect;
    void Start()
    {
        curLife = life;
    }
    public void Kill(int damage)
    {
        
        curLife -= damage;
        if(curLife <= 0)
        {
            PlayerControll.Instance.AddMoney();
            GetComponent<Animator>().SetTrigger("fall");
            gameObject.tag = "Untagged";
            transform.parent.gameObject.GetComponent<Enemy>().RemoveCount(gameObject);            
            Destroy(gameObject, 3);
            GameObject eff = Instantiate(deadEffect, new Vector3(transform.position.x, 1, transform.position.z), transform.rotation) as GameObject;
            Destroy(eff, 1);
        } 
        else
            GetComponent<Animator>().SetTrigger("hit");
    }
}
