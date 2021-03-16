using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculatingSchedules : MonoBehaviour
{
    public GameObject mainPanel;
    public Text outputText;
    public RectTransform panelContent;

    private RectTransform heightOutputText;
    private bool checkHeightOutputText;
    private float maxHeightOutputText;
    private int num;

    private void Update()
    {
        if(checkHeightOutputText)
            if (heightOutputText.rect.height > maxHeightOutputText)
            {
                maxHeightOutputText = heightOutputText.rect.height;
                panelContent.sizeDelta = new Vector2(panelContent.sizeDelta.x, maxHeightOutputText);
            }
    }

    public void Calculating()
    {
        mainPanel.SetActive(true);

        int[,] arr = ReadingData.adjacencyMatrix;
        int n = SizeMatrix.inpFieldSubjectsY.Count;
        int timeOne = SizeMatrix.itemTimeOne;
        int timeTwo = SizeMatrix.itemTimeTwo;
        string[] subject = new string[SizeMatrix.inpFieldSubjectsY.Count];
        for (int i = 0; i < subject.Length; i++)
            subject[i] = SizeMatrix.inpFieldSubjectsY[i].text;

        num = 1;
        outputText.text += "Первая смена: \n";
        Combinations(arr, n, timeOne, subject);
        if (n <= timeOne)
        {
            string[] tempSubject = new string[timeOne];
            for (int i = 0; i < subject.Length; i++)
                tempSubject[i] = subject[i];
            for (int i = subject.Length; i < timeOne; i++)
                tempSubject[i] = "-";
            Array.Sort(tempSubject);
            ShowAllCombinations(ref tempSubject, tempSubject.Length);
        }
        --num;
        outputText.text += "Общее количество: " + num.ToString() + "\n";

        num = 1;
        outputText.text += "Вторая смена: \n";
        Combinations(arr, n, timeTwo, subject);
        if (n <= timeTwo)
        {
            string[] tempSubject1 = new string[timeTwo];
            for (int i = 0; i < subject.Length; i++)
                tempSubject1[i] = subject[i];
            for (int i = subject.Length; i < timeTwo; i++)
                tempSubject1[i] = "-";
            Array.Sort(tempSubject1);
            ShowAllCombinations(ref tempSubject1, tempSubject1.Length);
        }
        --num;
        outputText.text += "Общее количество: " + num.ToString() + "\n";

        heightOutputText = outputText.GetComponent<RectTransform>();
        checkHeightOutputText = true;
    }
    public void Combinations(int[,] arr, int n, int m, string[] subject)
    {
        for (int i = n - m; i < (n / 2 + 1); i++)
        {
            if (i <= 0)
                i++;
            List<int> lastIndexes = new List<int>();
            for (int j = 0; j < n - 1; j++)
            {
                for (int k = j + 1; k < n; k++)
                {
                    if (CheckForIndex(j, lastIndexes))
                    {
                        if (j == n - 2)
                        {
                            k = lastIndexes[lastIndexes.Count - 1];
                            lastIndexes.RemoveAt(lastIndexes.Count - 1);
                            j = lastIndexes[lastIndexes.Count - 1];
                            lastIndexes.RemoveAt(lastIndexes.Count - 1);
                        }
                        break;
                    }
                    if (CheckForIndex(k, lastIndexes))
                        continue;
                    if (arr[j, k] == 1)
                    {
                        lastIndexes.Add(j);
                        lastIndexes.Add(k);
                        if (lastIndexes.Count == i * 2)
                        {
                            CreatedList(lastIndexes, n, m, i, subject);
                            lastIndexes.RemoveAt(lastIndexes.Count - 1);
                            lastIndexes.RemoveAt(lastIndexes.Count - 1);
                        }
                    }
                    if ((j == n - 2) && (k == n - 1))
                    {
                        if (lastIndexes.Count > 0)
                        {
                            k = lastIndexes[lastIndexes.Count - 1];
                            lastIndexes.RemoveAt(lastIndexes.Count - 1);
                            j = lastIndexes[lastIndexes.Count - 1];
                            lastIndexes.RemoveAt(lastIndexes.Count - 1);
                        }
                    }
                }
            }
        }
    }

    public void CreatedList(List<int> lastIndexes, int n, int m, int i, string[] subject)
    {
        int indexCombinations = 0;
        string[] arr = new string[m];

        for (int l = 0; l < n; l++)
        {
            if (!CheckForIndex(l, lastIndexes))
            {
                arr[indexCombinations] = subject[l];
                indexCombinations++;
            }
        }
        for (int k = 0; k < lastIndexes.Count; k = k + 2)
        {
            arr[indexCombinations] = "(" + subject[lastIndexes[k]] + "/" + subject[lastIndexes[k + 1]] + ")";
            indexCombinations++;
        }
        for (; indexCombinations < m; indexCombinations++)
            arr[indexCombinations] = "-";
        Array.Sort(arr);
        ShowAllCombinations(ref arr, m);
    }
    public bool CheckForIndex(int index, List<int> lastIndexes)
    {
        foreach (int lastIndex in lastIndexes)
        {
            if (lastIndex == index)
                return true;
        }
        return false;
    }
    public void ShowAllCombinations(ref string[] a, int n)
    {
        Print(ref a, n);
        while (NextSet(ref a, n))
            Print(ref a, n);
    }
    public void Swap(ref string[] a, int i, int j)
    {
        string s = a[i];
        a[i] = a[j];
        a[j] = s;
    }
    public bool NextSet(ref string[] a, int n)
    {
        int j = n - 2;
        while (j != -1 && String.Compare(a[j], a[j + 1]) >= 0)
            j--;
        if (j == -1)
            return false; // больше перестановок нет
        int k = n - 1;
        while (String.Compare(a[j], a[k]) >= 0)
            k--;
        Swap(ref a, j, k);
        int l = j + 1, r = n - 1; // сортируем оставшуюся часть последовательности
        while (l < r)
            Swap(ref a, l++, r--);
        return true;
    }
    public void Print(ref string[] a, int n)  // вывод перестановки
    {
        outputText.text += num.ToString() + ": " + "\n";
        num++;
        for (int i = 0; i < n; i++)
            outputText.text += a[i].ToString() + " \n";
    }
    static int Factorial(int x)
    {
        return (x == 0) ? 1 : x * Factorial(x - 1);
    }
}
