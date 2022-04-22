using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcoin : MonoBehaviour
{
    public static UIcoin Instance;   
    [SerializeField] float moveTime, waitToMoveTime;
    [SerializeField] float x, y, moneyScale;
    [SerializeField] Transform targetPos, parent;
    void Start()
    {
        if (Instance == null)
            Instance = this;
    }
    void Update()
    {
        
    }
    public void MoveOn(Transform pos, int count)
    {
        StartCoroutine(StartMove(pos, count));
    }
    IEnumerator StartMove(Transform pos, int count)
    {
        Vector3 stPos = Camera.main.WorldToScreenPoint(pos.position);
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = PoolControll.Instance.Spawn("Money");
            obj.transform.parent = parent;
            obj.GetComponent<RectTransform>().sizeDelta = new Vector3(moneyScale, moneyScale, 1);
            list.Add(obj);
            x = Random.Range(-x, x); //Для разброса при появлении
            y = Random.Range(-y, y);            
            obj.GetComponent<RectTransform>().position = new Vector3(stPos.x + x, stPos.y + y, stPos.z);   
            obj.SetActive(true);
        }
        for (int l = 0; l < list.Count; l++)
        {
            StartCoroutine(DoMove(list[l].transform));
            yield return new WaitForSeconds(waitToMoveTime);         
        }        
    }
    IEnumerator Scale()
    {
        targetPos.localScale = new Vector3(1.4f, 1.4f, 1.4f); //Перед этим скейл до 1f,1f,1f убрал, чтоб чище анимация пульсации была
        yield return new WaitForSeconds(0.05f);
        targetPos.localScale = new Vector3(1f, 1f, 1f);
    }
    private IEnumerator DoMove(Transform obj)
    {
        Vector2 startPosition = obj.localPosition;
        //yield return new WaitForSeconds(0.5f); Чтоб повисели, потом отлетели
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / moveTime);
            obj.localPosition = Vector2.Lerp(startPosition, targetPos.localPosition, fraction);
            yield return null;
        }
        PlayerControll.Instance.AddMoney();
        obj.gameObject.SetActive(false);
        StartCoroutine(Scale());
    }
}
