using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enem : MonoBehaviour
{
    public int life;
    public int curLife;
    public GameObject deadEffect;

    private void Awake()
    {
        curLife = life;
    }
    void Start()
    {        
        GetComponent<Animator>().SetTrigger("enemy");
    }
    public void Kill(int damage)
    {
        print("coll");
        curLife -= damage;
        transform.parent.gameObject.GetComponent<Enemy>().SetCount();

        if (curLife <= 0)
        {
            PlayerControll.Instance.AddMoney();
            GetComponent<Animator>().SetTrigger("fall");
            gameObject.tag = "Untagged";
            transform.parent.gameObject.GetComponent<Enemy>().RemoveCount();            
            Destroy(gameObject, 3);
            GameObject eff = Instantiate(deadEffect, new Vector3(transform.position.x, 1, transform.position.z), transform.rotation) as GameObject;
            Destroy(eff, 1);
        } 
        else
            GetComponent<Animator>().SetTrigger("hit");        
    }
}
