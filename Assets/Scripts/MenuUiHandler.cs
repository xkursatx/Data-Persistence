using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUiHandler : MonoBehaviour
{
    public Text BestScoreText;

    public void OnEndEdit(string value)
    {
        GameManager.Instance.CurrentName = value;
    }

    public void Startnew()
    {
        SceneManager.LoadScene("main");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
