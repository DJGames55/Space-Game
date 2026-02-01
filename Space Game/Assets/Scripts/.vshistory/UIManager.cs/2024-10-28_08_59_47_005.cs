using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;


    private void Start()
    {
        _input.PauseEvent += Pause;
        _input.ResumeEvent += Resume;
    }

    // Menu
    public GameObject Menu;
    #region Menu

    private void Pause()
    {
        
    }

    private void Resume()
    {

    }

    #endregion
}
