using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enem : MonoBehaviour
{
    public int life;
    public GameObject deadEffect;
    void Start()
    {
        
    }
    public void Kill(int damage)
    {
        
        life -= damage;
        if(life <= 0)
        {
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
