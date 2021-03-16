using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScenes : MonoBehaviour
{
    public void StartProgram()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ExitProgram()
    {
        Application.Quit();
    }

    public void BackMainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
}
