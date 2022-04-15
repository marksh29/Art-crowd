using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gener : MonoBehaviour
{
    public static Gener Instance;
    [SerializeField] float onTime, yy;
    [SerializeField] GameObject obj, confeti;
    [SerializeField] List<GameObject> list;
    [SerializeField] int count;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GameObject[] mon = GameObject.FindGameObjectsWithTag("Money");
        GameObject[] en = GameObject.FindGameObjectsWithTag("Enemy");
        count = mon.Length + en.Length;

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
        
    }
    public void StartEnd(int id)
    {
        StartCoroutine(End(id));
    }
    IEnumerator End(int id)
    {
        for (int i = 0; i < id; i++)
        {
            list[i].SetActive(true);
            confeti.transform.localPosition = new Vector3(confeti.transform.localPosition.x, list[i].transform.localPosition.y, confeti.transform.localPosition.z);
            yield return new WaitForSeconds(onTime);
        }
        confeti.SetActive(true);
        Controll.Instance.Set_state("Win");        
    }
}
