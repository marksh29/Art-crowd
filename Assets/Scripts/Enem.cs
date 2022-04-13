using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enem : MonoBehaviour
{
    public int life;

    void Start()
    {
        
    }
    public void Kill(int damage)
    {
        GetComponent<Animator>().SetTrigger("hit");
        life -= damage;
        if(life <= 0)
        {
            gameObject.tag = "Untagged";
            transform.parent.gameObject.GetComponent<Enemy>().RemoveCount(gameObject);            
            Destroy(gameObject, 1);
        }            
    }
}
