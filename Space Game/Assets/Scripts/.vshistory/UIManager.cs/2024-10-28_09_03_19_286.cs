using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;


    private void Start()
    {
        _input.ResumeEvent += Resume;
    }

    // Menu
    public GameObject Menu;
    #region Menu

    private void Pause()
    {
        Menu.SetActive(true);
        _input.SetUI();
    }

    private void Resume()
    {
        Menu.SetActive(false);
    }

    #endregion
}
