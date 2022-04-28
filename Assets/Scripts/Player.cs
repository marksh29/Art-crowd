
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] float moveTime;
    public int life;
    Vector3 targetPosition;
    IEnumerator corr;
    [SerializeField] float startScales, startShapes;
    float addScale, addShape;
    [SerializeField] Animator anim;
    [SerializeField] SkinnedMeshRenderer skin;
    [SerializeField] GameObject[] effect, manPrefab;
    [SerializeField] bool changeScaleForDamage;
    bool end;
    [SerializeField] GameObject deadEffect, goldEffect;

    [SerializeField] TextMeshPro lifeText;
    [HideInInspector] public Vector3 targetPos;
    bool move;
    Vector3 startScale;

    [SerializeField] float scaleTime;
    float extraScale, extraShape;
    void Awake()
    {
        life = life == 0 ? 1 : life;
        startScale = transform.localScale;
        SetPatamtr();
        
        //float scale = skin.GetBlendShapeWeight(0) - (addShape * (life - 1));
        //skin.SetBlendShapeWeight(0, scale < 0 ? 0 : scale);        
    }
    void SetPatamtr()
    {
        extraScale = startScales / 2;
        extraShape = startShapes / 2;
        addShape = startShapes + extraShape;
        addScale = startScales + extraScale;
    }

    private void Update()
    {
        if(move)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, moveTime * Time.deltaTime);
            if(transform.localPosition == targetPos)
            {
                move = false;
            }
        }    
    }
    public void SetAnimation(string name)
    {
        anim?.SetTrigger(name);
    }
    public void SetStarget(Vector3 target)
    {
        targetPos = target;
        if(corr != null)
            StopCoroutine(corr);
        targetPosition = new Vector3(target.x, transform.localPosition.y, target.z);
        move = true;
        //corr = DoMove((transform.localPosition - targetPosition).sqrMagnitude * moveTime);
        //StartCoroutine(corr);
    }
    private IEnumerator DoMove(float time)
    {
        Vector3 startPosition = transform.localPosition;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
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
            //if (coll.gameObject.tag == "Add")
            //{                
            //    Line.Instance.AddObj(coll.gameObject);
            //    coll.gameObject.transform.parent = transform.parent;
            //    coll.gameObject.GetComponent<Player>().Add(gameObject.transform);               
            //}
            if (coll.gameObject.tag == "Money")
            {
                GameObject eff = Instantiate(goldEffect, coll.gameObject.transform.position, transform.rotation) as GameObject;
                Destroy(eff, 1);
                UIcoin.Instance.MoveOn(coll.gameObject.transform, 1);
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
            if (coll.gameObject.tag == "DropScale")
            {
                int ct = coll.gameObject.transform.parent.GetComponent<Gate>().SetGate();
                StartCoroutine(Effect(1));
                DropToNull();
            }
            if (coll.gameObject.tag == "Skin")
            {
                ChangeMan(coll.gameObject.transform.parent.GetComponent<Gate>().SetSkinGate());
            }
            if (coll.gameObject.tag == "MoneyGate")
            {
                ChangeMan(coll.gameObject.transform.parent.GetComponent<Gate>().skinID);
                UIcoin.Instance.MoveOn(coll.gameObject.transform, coll.gameObject.transform.parent.GetComponent<Gate>().SetMoneyGate());
            }
        }       
    }

    void AddScale(Gate gate)
    {
        StartCoroutine(Effect(1));
        int cnt = gate.SetGate();       
        life += cnt;

        //float scale = skin.GetBlendShapeWeight(0) -(addShape);
        //skin.SetBlendShapeWeight(0, scale < 0 ? 0 : scale);
        
        Line.Instance.SetCount();
        lifeText.text = life.ToString();       
        AddScales(transform.parent, addScale);
    }
    void RemoveScale(Gate gate)
    {
        StartCoroutine(Effect(2));
        int cnt = life <= gate.count ? life -1 : Mathf.Abs(gate.SetGate());
        life -= cnt;
        if (life <= 0)
            Dead();
        else
        {
            AddScales(transform.parent, -addScale);
        }

        //float scale = skin.GetBlendShapeWeight(0) + (addShape);
        //skin.SetBlendShapeWeight(0, scale > 100 ? 100 : scale);   
        
        lifeText.text = life.ToString();       
    }   
    public void Damage(Enem enemy)
    {
        life -= enemy.Kill(life);    
        Line.Instance.SetCount();
        lifeText.text = life.ToString();

        if (changeScaleForDamage)
        {           
            float scale = skin.GetBlendShapeWeight(0) + addShape;
            skin.SetBlendShapeWeight(0, scale > 100 ? 100 : scale);
            AddScales(transform.parent, -addScale);
        }
        if (life <= 0)
        {
            Dead();
        }
    }
    void Dead()
    {
        lifeText.transform.parent.gameObject.SetActive(false);
        SetAnimation("fall");
        end = true;
        transform.parent = null;
        Line.Instance.RemoveObj(gameObject);
        Destroy(gameObject, 3);
        GameObject eff = Instantiate(deadEffect, new Vector3(transform.position.x, 1, transform.position.z), transform.rotation) as GameObject;
        Destroy(eff, 1);
    }

    public IEnumerator Effect(int id)
    {
        effect[id].SetActive(true);
        yield return new WaitForSeconds(1);
        effect[id].SetActive(false);
    }
    void AddScales(Transform parent, float count)
    {
        if (count > 0)
        {
            Shape(true);
            StartCoroutine(ScaleCorut(new Vector3(transform.localScale.x + (addScale / 9), transform.localScale.y + addScale, transform.localScale.z + (addScale / 6.5f)), false));
        }
        else
        {
            Shape(false);
            if (transform.localScale.y - addScale >= startScale.y)
                StartCoroutine(ScaleCorut(new Vector3(transform.localScale.x - (addScale / 9), transform.localScale.y - addScale, transform.localScale.z - (addScale / 6.5f)), true));
        }
    }
    public void Add(Transform pl)
    {        
        gameObject.transform.localRotation = Quaternion.Euler(0, pl.localRotation.y, 0);
        gameObject.tag = "Untagged";
        SetAnimation("move");        
        startScale = transform.localScale;
        StartCoroutine(Effect(0));
    }
    public void SpawnNewMan(int id)
    {
        GameObject obj = Instantiate(gameObject, transform.parent) as GameObject;       
        Line.Instance.AddObj(obj);
        obj.GetComponent<Player>().Drop();
        obj.transform.localPosition = new Vector3(Random.Range(-0.3f, 0.3f), transform.localPosition.y, transform.localPosition.z + 0.3f);        
    }
    public void Drop()
    {
        life = 1;
        if(skin != null)
            skin.SetBlendShapeWeight(0, 100);
        transform.localScale = new Vector3(0.077f, 0.7f, 0.1f);
        SetAnimation("move");
        Line.Instance.SetCount();
        StartCoroutine(Effect(0));
        lifeText.text = life.ToString();
    }

    public void LifeCount(bool id)
    {
        lifeText.transform.parent.gameObject.SetActive(id);
        lifeText.text = life.ToString();
    }

    void Shape(bool up)
    {
        if(skin != null)
        {
            switch (up)
            {
                case (true):
                    float scale = skin.GetBlendShapeWeight(0) - (addShape - extraShape);
                    StartCoroutine(LerpShape(scale < 0 ? 0 : scale, 1, false));
                    break;
                case (false):
                    float scale1 = skin.GetBlendShapeWeight(0) + (addShape - extraShape);
                    StartCoroutine(LerpShape(scale1 > 100 ? 100 : scale1, 1, false));
                    break;
            }
        }      
    }

    IEnumerator ScaleCorut(Vector3 targetScale, bool up)
    {
        Vector3 startScale = transform.localScale;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / scaleTime);
            transform.localScale = Vector3.Lerp(startScale, targetScale, fraction);
            yield return null;            
        }
        yield return new WaitForSeconds(0.2f);
        if(!up)
            transform.localScale -= new Vector3(extraScale / 9, extraScale, extraScale / 6.5f);
        else
            transform.localScale += new Vector3(extraScale / 9, extraScale, extraScale / 6.5f);
    }
    private IEnumerator LerpShape(float endValue, float duration, bool up)
    {
        float startValue = skin.GetBlendShapeWeight(0);
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float value = Mathf.Lerp(startValue, endValue, elapsed / duration);
            skin.SetBlendShapeWeight(0, value);
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        if (!up)
            skin.SetBlendShapeWeight(0, skin.GetBlendShapeWeight(0) + extraShape);
        else
            skin.SetBlendShapeWeight(0, skin.GetBlendShapeWeight(0) - extraShape);
    }
         
    public void DropToNull()
    {
        life = 1;
        StartCoroutine(ScaleCorut(startScale, true));
        if(skin != null)
            StartCoroutine(LerpShape(100, 1, true));
        lifeText.text = life.ToString();
        SetAnimation("move");
    }
    public void ChangeMan(int id)
    {
        GameObject obj = Instantiate(manPrefab[id], transform.parent) as GameObject;
        obj.transform.localScale = startScale;
        obj.transform.localPosition = transform.localPosition;
        obj.transform.localRotation = transform.localRotation;
        Line.Instance.AddObj(obj);
        obj.GetComponent<Player>().Add(gameObject.transform);
        Line.Instance.RemoveObj(gameObject);
        Destroy(gameObject);
    }
}
