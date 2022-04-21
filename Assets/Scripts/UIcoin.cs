using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcoin : MonoBehaviour
{
    public static UIcoin Instance;
    [SerializeField] Transform[] moveCoins;
    [SerializeField] Transform targetPos;
    [SerializeField] float moveTime;

    //float x;
    //float y;
    //float z;

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
        for (int i = 0; i < count; i++)
        {

            //x = Random.Range(-100, 100); Для разброса при появлении
            //y = Random.Range(-100, 100);
            //z = 0;
            //stPos = stPos + new Vector3(x, y, z);

            moveCoins[i].position = stPos;
            moveCoins[i].gameObject.SetActive(true);
            StartCoroutine(DoMove(moveCoins[i]));
            yield return new WaitForSeconds(0.1f);
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
