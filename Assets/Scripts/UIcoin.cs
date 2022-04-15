using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcoin : MonoBehaviour
{
    public static UIcoin Instance;
    [SerializeField] Transform[] moveCoins;
    [SerializeField] Transform targetPos;
    [SerializeField] float moveTime;
    void Start()
    {
        if (Instance == null)
            Instance = this;
    }
    void Update()
    {
        
    }
    public void MoveOn(Transform pos)
    {
        StartCoroutine(StartMove(pos));
    }
    IEnumerator StartMove(Transform pos)
    {
        Vector3 stPos = Camera.main.WorldToScreenPoint(pos.position);
        for (int i = 0; i < moveCoins.Length; i++)
        {
            moveCoins[i].position = stPos;
            moveCoins[i].gameObject.SetActive(true);
            StartCoroutine(DoMove(moveCoins[i]));
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator Scale()
    {
        targetPos.localScale = new Vector3(1f, 1f, 1f);
        targetPos.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        yield return new WaitForSeconds(0.1f);
        targetPos.localScale = new Vector3(1f, 1f, 1f);
    }
    private IEnumerator DoMove(Transform obj)
    {
        Vector2 startPosition = obj.localPosition;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / moveTime);
            obj.localPosition = Vector2.Lerp(startPosition, targetPos.localPosition, fraction);
            yield return null;
        }
        obj.gameObject.SetActive(false);
        StartCoroutine(Scale());
    }
}
