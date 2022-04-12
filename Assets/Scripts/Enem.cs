using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enem : MonoBehaviour
{
    public int life;
    public MeshRenderer mesh;

    void Start()
    {
        
    }
    public void Kill(int damage)
    {
        life -= damage;
        if(life <= 0)
        {
            transform.parent.gameObject.GetComponent<Enemy>().RemoveCount(gameObject);
        }            
    }
}
