using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject CreditsMenu;

    private void Awake()
    {
        Menu.SetActive(true);
        CreditsMenu.SetActive(false);
    }

    public void Play()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        if (Menu.activeSelf)
        {
            Menu.SetActive(false);
            CreditsMenu.SetActive(true);
        }
        else
        {
            Menu.SetActive(true);
            CreditsMenu.SetActive(false);
        }
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}
