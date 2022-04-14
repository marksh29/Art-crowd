using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Line : MonoBehaviour
{
    public static Line Instance;
    [SerializeField] List<GameObject> lineObj, gameObj;
    [SerializeField] RectTransform rect;
    [SerializeField] TextMeshPro countText;
    [SerializeField] GameObject prefab;
    [SerializeField] float spawnDistance;
    float dist;
    [SerializeField] bool cleareLine, updatePosition;
    bool lineOn;
    private void Awake()
    {
        if (Instance == null) Instance = this;

    }
    void Start()
    {
        End(false);
        countText.text = gameObj.Count.ToString();
    }
    public void StartGame(string name)
    {
        for (int i = 0; i < gameObj.Count; i++)
        {
            gameObj[i].GetComponent<Player>().SetAnimation(name);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CleareLine();
        }
        if (Input.GetMouseButton(0) && lineOn)
        {            
            if (lineObj.Count < gameObj.Count && (lineObj.Count == 0 || dist >= spawnDistance))
            {
                SpawnObj();
            }
            if (lineObj.Count > 0)
            {
                dist = (lineObj[lineObj.Count - 1].transform.position - Input.mousePosition).sqrMagnitude;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            OffLine();
            if (cleareLine)
                CleareLine();
        }
    }

    void SpawnObj()
    {
        GameObject obj = Instantiate(prefab) as GameObject;
        obj.transform.position = Input.mousePosition;
        obj.transform.parent = rect.gameObject.transform;
        lineObj.Add(obj);
        if (updatePosition)
            OffLine();
    }
    void CleareLine()
    {
        for (int i = 0; i < lineObj.Count; i++)
        {
            Destroy(lineObj[i]);
        }
        lineObj.Clear();
    }

    void SetPos()
    {
        int count = gameObj.Count > lineObj.Count ? lineObj.Count : gameObj.Count;
        for (int i = 0; i < count; i++)
        {
            float xx = 0.5f / (rect.sizeDelta.x / 2);
            float yy = 0.4f / (rect.sizeDelta.y / 2);
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
    }
    public void Off()
    {
        lineOn = false;
    }

    public void AddObj(GameObject obj)
    {
        gameObj.Add(obj);
        countText.text = gameObj.Count.ToString();
    }
    public void RemoveObj(GameObject obj)
    {
        gameObj.Remove(obj);
        countText.text = gameObj.Count.ToString();
        if (gameObj.Count <= 0)
           PlayerControll.Instance.Lose();
    }

    public void End(bool state)
    {
        countText.gameObject.transform.parent.gameObject.SetActive(state);
    }
}
