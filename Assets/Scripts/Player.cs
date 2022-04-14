using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveTime;
    [SerializeField] int life;
    Vector3 targetPosition;
    IEnumerator corr;

    [SerializeField] float addScale, addShape;
    [SerializeField] Animator anim;
    [SerializeField] SkinnedMeshRenderer skin;
    [SerializeField] GameObject[] effect;
    [SerializeField] bool changeScaleForDamage;
    bool end;

    void Start()
    {
        life = life == 0 ? 1 : life;
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
                //coll.gameObject.transform.rotation = Quaternion.Euler(0,0,0);
                coll.gameObject.tag = "Untagged";
                Line.Instance.AddObj(coll.gameObject);
                coll.gameObject.transform.parent = transform.parent;
                coll.gameObject.GetComponent<Player>().SetAnimation("move");
                StartCoroutine(coll.gameObject.GetComponent<Player>().Effect(1));
            }
            if (coll.gameObject.tag == "Boost")
            {
                AddScale();
            }
        }       
    }

    void AddScale()
    {
        StartCoroutine(Effect(0));
        life++;
        //transform.localScale += new Vector3(addScale, addScale, addScale);
        float scale = skin.GetBlendShapeWeight(0) - addShape;
        skin.SetBlendShapeWeight(0, scale < 0 ? 0 : scale);
    }
    //private void OnCollisionEnter(Collision coll)
    //{
    //    if (coll.gameObject.tag == "Enemy")
    //    {
    //        Damage(coll.gameObject.GetComponent<Enem>());   
    //    }
    //    if (coll.gameObject.tag == "Wall")
    //    {
    //        Line.Instance.RemoveObj(gameObject);
    //        Destroy(gameObject);
    //    }
    //    if (coll.gameObject.tag == "Add")
    //    {
    //        coll.gameObject.tag = "Untagged";
    //        Line.Instance.AddObj(coll.gameObject);
    //        coll.gameObject.transform.parent = transform.parent;           
    //    }
    //}
    public void Damage(Enem enemy)
    {
        enemy.Kill(life >= enemy.life ? enemy.life : life);
        life -= life >= enemy.life ? enemy.life : life;

        if(changeScaleForDamage)
        {
            //transform.localScale -= new Vector3(addScale, addScale, addScale);
            float scale = skin.GetBlendShapeWeight(0) + addShape;
            skin.SetBlendShapeWeight(0, scale > 100 ? 100 : scale);
        } 
        
        if (life <= 0)
        {
            SetAnimation("hit");
            end = true;            
            transform.parent = null;
            Line.Instance.RemoveObj(gameObject);
            Destroy(gameObject, 1);
        }
    }
    public IEnumerator Effect(int id)
    {
        effect[id].SetActive(true);
        yield return new WaitForSeconds(1);
        effect[id].SetActive(false);
    }
}
