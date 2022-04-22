using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Enem : MonoBehaviour
{
    public int life, curLife;
    public float addScale, addShape;
    public GameObject deadEffect;
    [SerializeField] SkinnedMeshRenderer skin;
    [SerializeField] TextMeshPro lifeText;
    private void Awake()
    {
        curLife = life;
        transform.localScale += new Vector3(addScale, addScale, addScale);
        lifeText.text = curLife.ToString();
    }
    void Start()
    {        
        GetComponent<Animator>().SetTrigger("enemy");
    }

    public int Kill(int damage)
    {       
        curLife -= damage;
        transform.parent.gameObject.GetComponent<Enemy>().SetCount();
        lifeText.text = curLife.ToString();

        if (curLife <= 0)
        {
            lifeText.transform.parent.gameObject.SetActive(false);
            GetComponent<Animator>().SetTrigger("fall");
            gameObject.tag = "Untagged";
            transform.parent.gameObject.GetComponent<Enemy>().RemoveCount();           
            Destroy(gameObject, 3);
            GameObject eff = Instantiate(deadEffect, new Vector3(transform.position.x, 1, transform.position.z), transform.rotation) as GameObject;
            Destroy(eff, 1);
            UIcoin.Instance.MoveOn(gameObject.transform, life);
        }
        else
            GetComponent<Animator>().SetTrigger("hit");
        return (curLife + damage);
    } 
    public void CounterOff(bool id)
    {
        lifeText.gameObject.SetActive(id);
    }
}
