using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] List<GameObject> lineObj, gameObj;

    [SerializeField] List<GameObject> lineCount;
    [SerializeField] GameObject prefab;
    [SerializeField] float spawnDistance;
    float dist;
    [SerializeField] bool lineOn;
    void Start()
    {
        
    }
    void Update()
    {                  
        if (Input.GetMouseButton(0) && lineOn)
        {
            if (lineObj.Count > 0)
            {
                dist = (lineObj[lineObj.Count - 1].transform.position - Input.mousePosition).sqrMagnitude;
            }
            if (lineObj.Count == 0 || dist >= spawnDistance)
            {
                SpawnObj();
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            OffLine();           
        }
    }

    void SpawnObj()
    {
        GameObject obj = Instantiate(prefab) as GameObject;
        obj.transform.position = Input.mousePosition;
        obj.transform.parent = transform;
        lineObj.Add(obj);
    }
    void SetPos()
    {
        int count = gameObj.Count > lineObj.Count ? lineObj.Count : gameObj.Count;
        for (int i = 0; i < count; i++)
        {
            float xx = 0.5f / (GetComponent<RectTransform>().sizeDelta.x / 2);
            float yy = 0.4f / (GetComponent<RectTransform>().sizeDelta.y / 2);
            Vector3 newPos = new Vector3(lineObj[i].GetComponent<RectTransform>().anchoredPosition.x * xx, 1, lineObj[i].GetComponent<RectTransform>().anchoredPosition.y * yy);
            gameObj[i].GetComponent<Player>().SetStarget(newPos);
            //gameObj[i].transform.localPosition = newPos;
        }
    }
    public void SetLine()
    {
        lineOn = true;

    }
    public void OffLine()
    {
        SetPos();       
        for (int i = 0; i < lineObj.Count; i++)
        {
            Destroy(lineObj[i]);
        }
        lineObj.Clear();
    }
    public void Off()
    {
        lineOn = false;
    }
}
