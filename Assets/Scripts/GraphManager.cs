using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphManager : MonoBehaviour
{
    public GameObject prefVertexGraph;
    public GameObject prefEdgeGraph;

    private int[,] adjacencyMatrix;
    private List<int> indexHeight = new List<int>();
    private List<int> indexWidht = new List<int>();
    private List<GameObject> createdVertexGraph = new List<GameObject>();
    private float widthVertexGraph;
    private bool createVertexConnections;
    private bool checkIntersectionVertex;
    private int quantityVertex;
    private int counterIndex;
    void Start()
    {
        adjacencyMatrix = new int[,] { { 0, 1, 1, 1 }, { 1, 0, 1, 1 }, { 1, 1, 0, 1 }, { 1, 1, 1, 0 } };
        quantityVertex = (int)Mathf.Sqrt((float)adjacencyMatrix.Length);
        widthVertexGraph = prefVertexGraph.GetComponent<RectTransform>().rect.width;

        for (int i = 0; i < (int)Mathf.Sqrt((float)adjacencyMatrix.Length); i++)
        {
            for (int j = 0; j < (int)Mathf.Sqrt((float)adjacencyMatrix.Length); j++)
            {
                if (adjacencyMatrix[i, j] == 1)
                {
                    indexHeight.Add(i);
                    indexWidht.Add(j);
                }
            }
        }
    }

    void Update()
    {
        while(quantityVertex != 0)
        {
            GameObject vertexGraph = Instantiate(prefVertexGraph);
            vertexGraph.transform.SetParent(transform);
            vertexGraph.transform.localScale = new Vector3(1, 1, 1);
            vertexGraph.transform.localPosition = new Vector2(vertexGraph.transform.localPosition.x, -createdVertexGraph.Count * widthVertexGraph);

            createdVertexGraph.Add(vertexGraph);
            quantityVertex--;
        }
        if (indexHeight.Count > counterIndex)
        {
            if (!CheckingIntersectionVertex(indexHeight[counterIndex], indexWidht[counterIndex]))
                CheckingIntersectionVertex(indexHeight[counterIndex], indexWidht[counterIndex]);
        }
        else
            checkIntersectionVertex = true;

        if (quantityVertex == 0 && !createVertexConnections && checkIntersectionVertex)
            VertexConnections();
    }

    private void VertexConnections()
    {
        createVertexConnections = true;
        for (int i = 0; i < (int)Mathf.Sqrt((float)adjacencyMatrix.Length); i++)
        {
            for (int j = 0; j < (int)Mathf.Sqrt((float)adjacencyMatrix.Length); j++)
            {
                if (adjacencyMatrix[i, j] == 1)
                {
                    GameObject edgeGraph = Instantiate(prefEdgeGraph);
                    edgeGraph.transform.SetParent(transform);
                    edgeGraph.transform.localScale = new Vector3(1, 1, 1);
                    LineRenderer lineGraph = edgeGraph.GetComponent<LineRenderer>();
                    lineGraph.SetPosition(0, createdVertexGraph[i].transform.localPosition);
                    lineGraph.SetPosition(1, createdVertexGraph[j].transform.localPosition);
                }
                else
                {

                }
            }
        }
    }

    private bool CheckingIntersectionVertex(int i, int j)
    {
        RaycastHit2D[] hit2Ds = Physics2D.LinecastAll(createdVertexGraph[i].transform.position, createdVertexGraph[j].transform.position);
        if (hit2Ds.Length != 0)
        {
            if (hit2Ds.Length > 2)
                createdVertexGraph[j].transform.localPosition = new Vector2(createdVertexGraph[j].transform.localPosition.x + widthVertexGraph, createdVertexGraph[j].transform.localPosition.y);
            else
            {
                counterIndex++;
                return true;
            }
        }
        return false;
    }
}
