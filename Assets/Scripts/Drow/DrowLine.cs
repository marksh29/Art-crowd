using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowLine : MonoBehaviour
{
    public static DrowLine Instance;

    //[SerializeField] GameObject linePrefab;
    //private GameObject currentLine;

   // private LineRenderer lineRenderer;
   
    public GameObject linePrefab, currentLine;
    LineRenderer lineRenderer;
    public List<Vector2> fingerPositions;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Controll.Instance._state == "Game")
        {            
            CreateLine();
        }
        if (Input.GetMouseButton(0) && Controll.Instance._state == "Game")
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > 0.1f)
                UpdateLine(tempFingerPos);
        }
        if (Input.GetMouseButtonUp(0))
        {
            fingerPositions.Clear();
        }
    }
    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
    }

    void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        fingerPositions.Clear();
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
    } 
}
