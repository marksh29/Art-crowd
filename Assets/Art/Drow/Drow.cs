using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drow : MonoBehaviour
{
    [SerializeField] Line line;
    [SerializeField] Camera cam;
    [SerializeField] GameObject linePrefab, currentLine;
    LineRenderer lineRenderer;
    public List<Vector2> fingerPositions;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && line.lineOn)
        {
            CleareLine();
            CreateLine();
        }
        if (Input.GetMouseButton(0) && line.lineOn && (line.lineObj.Count < line.gameObj.Count) && line.lineMove)
        {
            Vector2 tempFingerPos = cam.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > 0.1f)
                UpdateLine(tempFingerPos);            
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Line.Instance.cleareLine)
                CleareLine();
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
        fingerPositions.Add(cam.ScreenToWorldPoint(Input.mousePosition));
        fingerPositions.Add(cam.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
    }
    void CleareLine()
    {
        fingerPositions.Clear();
        if (currentLine != null)
            Destroy(currentLine);
    }
}
