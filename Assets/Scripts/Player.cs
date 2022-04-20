using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveTime;
    public int life;
    Vector3 targetPosition;
    IEnumerator corr;

    [SerializeField] float addScale, addShape;
    [SerializeField] Animator anim;
    [SerializeField] SkinnedMeshRenderer skin;
    [SerializeField] GameObject[] effect;
    [SerializeField] bool changeScaleForDamage;
    bool end;
    [SerializeField] GameObject deadEffect, goldEffect;
    [SerializeField] Material mat;
    void Start()
    {
        life = life == 0 ? 1 : life;

        float scale = skin.GetBlendShapeWeight(0) - (addShape * (life - 1));
        skin.SetBlendShapeWeight(0, scale < 0 ? 0 : scale);
        AddScales(transform.parent, (addScale * (life - 1)));
    }
    private void Update()
    {
        
    }
    public void SetAnimation(string name)
    {
        anim?.SetTrigger(name);
    }
    public void SetStarget(Vector3 target)
    {
        if(corr != null)
            StopCoroutine(corr);

        targetPosition = new Vector3(target.x, transform.localPosition.y, target.z);
        corr = DoMove();
        StartCoroutine(corr);
    }
    private IEnumerator DoMove()
    {
        Vector3 startPosition = transform.localPosition;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / moveTime);
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, fraction);
            yield return null;
        }
    }
    private void OnTriggerEnter(Collider coll)
    {
        if (!end)
        {
            if (coll.gameObject.tag == "Fall")
            {
                end = true;
                SetAnimation("fall");
                StopCoroutine(corr);
                gameObject.layer = 6;
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().useGravity = true;
                Line.Instance.RemoveObj(gameObject);
                Destroy(gameObject, 2);
            }

            if (coll.gameObject.tag == "Enemy")
            {
                Damage(coll.gameObject.GetComponent<Enem>());
            }
            if (coll.gameObject.tag == "Wall")
            {
                transform.parent = null;
                SetAnimation("fall");
                Line.Instance.RemoveObj(gameObject);
                Destroy(gameObject, 3);
            }

            if (coll.gameObject.tag == "Add")
            {                
                Line.Instance.AddObj(coll.gameObject);
                coll.gameObject.transform.parent = transform.parent;
                coll.gameObject.GetComponent<Player>().Add(gameObject.transform);
            }
            if (coll.gameObject.tag == "Money")
            {
                GameObject eff = Instantiate(goldEffect, coll.gameObject.transform.position, transform.rotation) as GameObject;
                Destroy(eff, 1);
                UIcoin.Instance.MoveOn(coll.gameObject.transform, 1);
                //PlayerControll.Instance.AddMoney();
                coll.gameObject.SetActive(false);
            }
            if (coll.gameObject.tag == "Boost")
            {
                if(coll.gameObject.transform.parent.gameObject.GetComponent<Gate>().count > 0)
                    AddScale(coll.gameObject.transform.parent.gameObject.GetComponent<Gate>());
                else
                    RemoveScale(coll.gameObject.transform.parent.gameObject.GetComponent<Gate>());
            }
            if (coll.gameObject.tag == "Mnoj")
            {
                SpawnNewMan(coll.gameObject.transform.parent.gameObject.GetComponent<Gate>().SetGate());
            }
        }       
    }

    void AddScale(Gate gate)
    {
        StartCoroutine(Effect(0));
        int cnt = gate.SetGate();       
        life += cnt;
        float scale = skin.GetBlendShapeWeight(0) -(addShape * cnt);
        skin.SetBlendShapeWeight(0, scale < 0 ? 0 : scale);
        AddScales(transform.parent, addScale);
        Line.Instance.SetCount();
    }
    void RemoveScale(Gate gate)
    {
        StartCoroutine(Effect(0));
        int cnt = life <= gate.count ? life -1 : Mathf.Abs(gate.SetGate());
        life -= cnt;        
        float scale = skin.GetBlendShapeWeight(0) + (addShape * cnt);
        skin.SetBlendShapeWeight(0, scale > 100 ? 100 : scale);
        AddScales(transform.parent, -addScale);
    }   
    public void Damage(Enem enemy)
    {
        enemy.Kill(life);
        life -= enemy.life;
        Line.Instance.SetCount();

        if (changeScaleForDamage)
        {           
            float scale = skin.GetBlendShapeWeight(0) + addShape;
            skin.SetBlendShapeWeight(0, scale > 100 ? 100 : scale);
            AddScales(transform.parent, -addScale);
        } 
        
        if (life <= 0)
        {
            SetAnimation("fall");
            end = true;            
            transform.parent = null;
            Line.Instance.RemoveObj(gameObject);
            Destroy(gameObject, 3);
            GameObject eff = Instantiate(deadEffect, new Vector3(transform.position.x, 1 ,transform.position.z), transform.rotation) as GameObject;
            Destroy(eff, 1);
        }
    }
    public IEnumerator Effect(int id)
    {
        effect[id].SetActive(true);
        yield return new WaitForSeconds(1);
        effect[id].SetActive(false);
    }
    void AddScales(Transform parent, float count)
    {
        transform.parent = parent.parent;
        transform.localScale += new Vector3(count, count, count);
        transform.parent = parent;
    }
    public void Add(Transform pl)
    {
        skin.sharedMaterial = mat;
        gameObject.transform.localRotation = Quaternion.Euler(0, pl.localRotation.y, 0);
        gameObject.tag = "Untagged";
        SetAnimation("move");
        StartCoroutine(Effect(1));
    }

    public void SpawnNewMan(int id)
    {
        GameObject obj = Instantiate(gameObject, transform.parent) as GameObject;       
        Line.Instance.AddObj(obj);
        obj.GetComponent<Player>().Drop();
        obj.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 0.3f);
    }
    public void Drop()
    {
        life = 1;
        skin.SetBlendShapeWeight(0, 100);
        transform.localScale = new Vector3(0.077f, 0.7f, 0.1f);
        SetAnimation("move");
        Line.Instance.SetCount();
    }
}
