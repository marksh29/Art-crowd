using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveTime;
    [SerializeField] Vector3 targetPosition;
    
    void Start()
    {
        
    }
    private void Update()
    {
        
    }
    public void SetStarget(Vector3 target)
    {
        targetPosition = new Vector3(target.x, transform.localPosition.y, target.z);
        StartCoroutine(DoMove());
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
}
