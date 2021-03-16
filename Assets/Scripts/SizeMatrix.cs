using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeMatrix : MonoBehaviour
{
    public static int itemTimeOne;
    public static int itemTimeTwo;
    public static List<InputField> inpFieldSubjectsY = new List<InputField>();
    public GameObject InputFieldItemTimeOne;
    public GameObject InputFieldItemTimeTwo;
    public GameObject prefInputFieldMatrix;
    public GameObject prefinputFieldSubject;
    public GameObject inputFieldsMatrix;
    public GameObject frontPanel;
    public GameObject mainPanel;
    public GameObject InputFieldSubjects;
    public InputField sizeMatrix;
    public Text outputText;

    private GridLayoutGroup groupMatrix;
    private List<InputField> inpFieldSubjectsX = new List<InputField>();
    private bool existencesListSubjects;
    private void Start()
    {
        groupMatrix = inputFieldsMatrix.GetComponent<GridLayoutGroup>();
    }

    public void InputSizeMatrix()
    {
        if (sizeMatrix.text != "")
        {
            Vector2 posInpFieldSubject;
            float sizeCellX = groupMatrix.cellSize.x + groupMatrix.spacing.x;
            float sizeCellY = groupMatrix.cellSize.y + groupMatrix.spacing.y;
            float sizeArray = Convert.ToInt32(sizeMatrix.text);
            if(sizeArray % 2 != 0)
                posInpFieldSubject = new Vector2(sizeCellX * ((sizeArray / 2) - 0.5f), sizeCellY * ((sizeArray / 2) - 0.5f));
            else
                posInpFieldSubject = new Vector2(sizeCellX * ((sizeArray / 2) - 1) + sizeCellX/2, sizeCellY * ((sizeArray / 2) - 1) + sizeCellY/2);

            groupMatrix.constraintCount = (int)sizeArray;
            for (int i = 0; i < sizeArray * sizeArray; i++)
            {
                GameObject inpFieldMatrix = Instantiate(prefInputFieldMatrix, inputFieldsMatrix.transform);
            }
            for (int i = 0; i < sizeArray; i++)
            {
                Vector2 posInpFieldSubjectY = posInpFieldSubject;
                Vector2 posInpFieldSubjectX = posInpFieldSubject;

                float shiftY = i * sizeCellY;
                float shiftX = i * sizeCellX;

                GameObject inpFieldSubjectY = Instantiate(prefinputFieldSubject, InputFieldSubjects.transform);
                GameObject inpFieldSubjectX = Instantiate(prefinputFieldSubject, InputFieldSubjects.transform);

                posInpFieldSubjectY.x = posInpFieldSubject.x - ((sizeArray + 2) * sizeCellX);
                posInpFieldSubjectY.y = posInpFieldSubject.y - shiftY;
                inpFieldSubjectY.transform.localPosition = posInpFieldSubjectY;

                posInpFieldSubjectX.x = posInpFieldSubject.x - ((sizeArray - 1) * sizeCellX) + shiftX;
                posInpFieldSubjectX.y = posInpFieldSubject.y + (3 * sizeCellY);
                inpFieldSubjectX.transform.localPosition = posInpFieldSubjectX;
                inpFieldSubjectX.transform.eulerAngles = new Vector3(0, 0, 90);

                inpFieldSubjectX.GetComponent<InputField>().readOnly = true;
                inpFieldSubjectX.GetComponent<InputField>().placeholder.enabled = false;
                
                inpFieldSubjectsX.Add(inpFieldSubjectX.GetComponent<InputField>());
                inpFieldSubjectsY.Add(inpFieldSubjectY.GetComponent<InputField>());
            }
            itemTimeOne = Convert.ToInt32(InputFieldItemTimeOne.GetComponent<InputField>().text);
            itemTimeTwo = Convert.ToInt32(InputFieldItemTimeTwo.GetComponent<InputField>().text);
            existencesListSubjects = true;
            frontPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if(existencesListSubjects)
            for(int i = 0; i < inpFieldSubjectsY.Count; i++)
                inpFieldSubjectsX[i].text = inpFieldSubjectsY[i].text;
    }

    public void ResettingData()
    {
        existencesListSubjects = false;
        int sizeArray = inputFieldsMatrix.transform.childCount;
        for (int i = 0; i < sizeArray; i++)
            Destroy(inputFieldsMatrix.transform.GetChild(i).gameObject);

        for (int i = 0; i < inpFieldSubjectsX.Count; i++)
        {
            Destroy(inpFieldSubjectsX[i].gameObject);
            Destroy(inpFieldSubjectsY[i].gameObject);
        }

        inpFieldSubjectsX.Clear();
        inpFieldSubjectsY.Clear();

        for (int i = 0; i < (int)Math.Pow((double)sizeArray, 0.5); i++)
            for (int j = 0; j < (int)Math.Pow((double)sizeArray, 0.5); j++)
            {
                try
                {
                    ReadingData.adjacencyMatrix[i, j] = 0;
                }
                catch(Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
        itemTimeOne = 0;
        itemTimeTwo = 0;
        outputText.text = "";
        frontPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

}
