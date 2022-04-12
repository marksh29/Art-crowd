using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveTime;
    [SerializeField] int life;
    [SerializeField] Vector3 targetPosition;
    IEnumerator corr;
    void Start()
    {
        life = life == 0 ? 1 : life;
    }
    private void Update()
    {
        
    }
    public void SetStarget(Vector3 target)
    {
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
        if (coll.gameObject.tag == "Fall")
        {
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
            Line.Instance.RemoveObj(gameObject);
            Destroy(gameObject);
        }

        if (coll.gameObject.tag == "Add")
        {
            coll.gameObject.tag = "Untagged";
            Line.Instance.AddObj(coll.gameObject);
            coll.gameObject.transform.parent = transform.parent;
        }       
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

        if(life <= 0)
        {
            Line.Instance.RemoveObj(gameObject);
            Destroy(gameObject);
        }
    }
}
