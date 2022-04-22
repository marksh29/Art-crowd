using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Gener : MonoBehaviour
{
    bool end;
    public static Gener Instance;
    [SerializeField] float onTime, yy;
    [SerializeField] GameObject obj, confeti;
    [SerializeField] List<GameObject> list;
    [SerializeField] int count;

    [SerializeField] CinemachineVirtualCamera gameCam, cam;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GameObject[] mon = GameObject.FindGameObjectsWithTag("Money");
        GameObject[] en = GameObject.FindGameObjectsWithTag("Enemy");

        count = mon.Length;
        for (int i = 0; i < en.Length; i++)
        {
            count += en[i].GetComponent<Enem>().life;
        }        

        for (int i = 0; i < count; i++)
        {
            GameObject sp = Instantiate(obj, list[0].transform.parent) as GameObject;
            sp.transform.localPosition = new Vector3(list[0].transform.localPosition.x, list[i].transform.localPosition.y + yy, list[0].transform.localPosition.z);
            list.Add(sp);
            sp.SetActive(false);
        }
    }
    void Update()
    {
        if (end)
        {
           
        }
    }

    public void StartEnd(int id)
    {
        gameCam.gameObject.SetActive(false);
        cam.gameObject.SetActive(true);
        StartCoroutine(End(id));    
        cam.LookAt = null;
        end = true;
    }
    //private IEnumerator DoMove(float time)
    //{
    //    var transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
    //    //transposer.m_FollowOffset = endPos;

    //    Vector3 startPosition = transposer.m_FollowOffset;

    //    float startTime = Time.realtimeSinceStartup;
    //    float fraction = 0f;
    //    while (fraction < 1f)
    //    {
    //        fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
    //        transposer.m_FollowOffset = Vector3.Lerp(startPosition, endPos, fraction);
    //        yield return null;
    //    }
    //}

    IEnumerator End(int id)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].SetActive(true);
            cam.Follow = list[i].transform;           
            confeti.transform.localPosition = new Vector3(confeti.transform.localPosition.x, list[i].transform.localPosition.y, confeti.transform.localPosition.z);
            yield return new WaitForSeconds(onTime);
        }
        confeti.SetActive(true);
        Controll.Instance.Set_state("Win");        
    }


}
