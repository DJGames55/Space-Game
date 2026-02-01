using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(1);
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}
