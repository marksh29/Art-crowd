using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Line : MonoBehaviour
{
    public static Line Instance;
    public List<GameObject> lineObj, gameObj;
    [SerializeField] RectTransform rect;
    [SerializeField] TextMeshPro countText;
    [SerializeField] GameObject prefab;
    [SerializeField] float spawnDistance, spawnScale, addZ;
    float dist;
    public bool cleareLine, updatePosition;
    [SerializeField] public bool lineOn, lineMove;
    [SerializeField] GameObject tutor;
    [SerializeField] bool massCounter;
         
    private void Awake()
    {        
        if (Instance == null) Instance = this;        
    }
    void Start()
    {
        for (int i = 0; i < gameObj.Count; i++)
        {
            gameObj[i].GetComponent<Player>().LifeCount(!massCounter);
        }
        SetCount();
    }
    public void StartGame(string name)
    {
        countText.transform.parent.gameObject.SetActive(massCounter);
        for (int i = 0; i < gameObj.Count; i++)
        {
            gameObj[i].GetComponent<Player>().SetAnimation(name);
        }
    }

    void Update()
    {        
        if (Input.GetMouseButtonDown(0) && lineOn && (Controll.Instance._state == "Menu" || Controll.Instance._state == "Game"))
        {
            lineMove = true;
            tutor.SetActive(false);           
            CleareLine();
        }
        if (Input.GetMouseButton(0) && lineOn && (Controll.Instance._state == "Menu" || Controll.Instance._state == "Game") && lineMove)
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
        if(Input.GetMouseButtonUp(0) && (Controll.Instance._state == "Menu" || Controll.Instance._state == "Game"))
        {
            if (lineObj.Count > 0)
            {
                if (Tutorial.Instance != null)
                {
                    Tutorial.Instance.TutorialOff();
                    //lineOn = false;
                }
                if(Controll.Instance._state == "Menu")
                    Controll.Instance.StartLevel();

                SetPos();
                if (cleareLine)
                    CleareLine();
            }
            lineMove = false;
        }
    }
   
    void SpawnObj()
    {
        GameObject obj = Instantiate(prefab) as GameObject;
        obj.transform.parent = rect.gameObject.transform;
        obj.transform.position = Input.mousePosition;      
        obj.GetComponent<RectTransform>().sizeDelta = new Vector2(spawnScale, spawnScale);
        lineObj.Add(obj);
        if (updatePosition)
            SetPos();
    }
    public void CleareLine()
    {
        for (int i = 0; i < lineObj.Count; i++)
        {
            Destroy(lineObj[i]);
        }
        lineObj.Clear();
    }  

    void SetPos()
    {
        if(Controll.Instance._state == "Game")
        {
            int count = gameObj.Count > lineObj.Count ? lineObj.Count : gameObj.Count;
            for (int i = 0; i < count; i++)
            {
                float xx = 0.5f / (rect.sizeDelta.x / 2);
                float yy = 0.4f / (rect.sizeDelta.y / 2);
                Vector3 newPos = new Vector3(lineObj[i].GetComponent<RectTransform>().anchoredPosition.x * xx, gameObj[i].transform.localPosition.y, lineObj[i].GetComponent<RectTransform>().anchoredPosition.y * yy);
                gameObj[i].GetComponent<Player>().SetStarget(newPos);
            }
            if (gameObj.Count > lineObj.Count && !updatePosition)
            {
                for (int i = count; i < gameObj.Count; i++)
                {
                    Player curPl = gameObj[i - lineObj.Count].GetComponent<Player>();
                    Vector3 newPos = new Vector3(curPl.targetPos.x, gameObj[i].transform.localPosition.y, curPl.targetPos.z - addZ);
                    gameObj[i].GetComponent<Player>().SetStarget(newPos);
                }
            }
        }       
    }
    public void SetLine()
    {
        lineOn = true;
    }
   
    public void Off()
    {
        lineOn = false;
    }

    public void AddObj(GameObject obj)
    {
        gameObj.Add(obj);
        obj.GetComponent<Player>().LifeCount(!massCounter);
        SetCount();        
    }
    public void RemoveObj(GameObject obj)
    {
        gameObj.Remove(obj);       
        if (gameObj.Count <= 0)
           PlayerControll.Instance.Lose();
        SetCount();
    }

    public void End(bool state)
    {
        countText.gameObject.transform.parent.gameObject.SetActive(state);
    }
    public void SetCount()
    {
        int ct = new int();
        for (int i = 0; i < gameObj.Count; i++)
        {
            ct += gameObj[i].GetComponent<Player>().life;
        }
        countText.text = ct.ToString();
    }
}
