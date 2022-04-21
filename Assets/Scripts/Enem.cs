using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enem : MonoBehaviour
{
    public int life, curLife;
    public float addScale, addShape;
    public GameObject deadEffect;
    [SerializeField] SkinnedMeshRenderer skin;

    private void Awake()
    {
        curLife = life;
        transform.localScale += new Vector3(addScale, addScale, addScale);

    }
    void Start()
    {        
        GetComponent<Animator>().SetTrigger("enemy");
    }

    public int Kill(int damage)
    {       
        curLife -= damage;
        transform.parent.gameObject.GetComponent<Enemy>().SetCount();

        if (curLife <= 0)
        {
            //PlayerControll.Instance.AddMoney();
            GetComponent<Animator>().SetTrigger("fall");
            gameObject.tag = "Untagged";
            transform.parent.gameObject.GetComponent<Enemy>().RemoveCount();
            UIcoin.Instance.MoveOn(gameObject.transform, 3);
            Destroy(gameObject, 3);
            GameObject eff = Instantiate(deadEffect, new Vector3(transform.position.x, 1, transform.position.z), transform.rotation) as GameObject;
            Destroy(eff, 1);
        }
        else
            GetComponent<Animator>().SetTrigger("hit");
        return (curLife + damage);
    }
    //public void Kill(int damage)
    //{
                
    //}
}
