using System;
using UnityEngine;
using UnityEngine.UI;

public class ReadingData : MonoBehaviour
{
    public static int[,] adjacencyMatrix;
    public GameObject inputFieldsMatrix;
    private int sizeMatrix;

    public void ReadingDataMatrix()
    {
        int counter = 0;
        sizeMatrix = (int)Math.Pow((double)inputFieldsMatrix.transform.childCount, 0.5);
        adjacencyMatrix = new int[sizeMatrix, sizeMatrix];
        for (int i = 0; i < sizeMatrix; i++)
            for (int j = 0; j < sizeMatrix; j++)
            {
                adjacencyMatrix[i, j] = Convert.ToInt32(inputFieldsMatrix.transform.GetChild(counter).GetComponent<InputField>().text);
                counter++;
            }
    }

}
