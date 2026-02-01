using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Menu
    public GameObject Menu;
    #region Menu

    private void Pause()
    {
        Menu.SetActive(true);
    }

    private void Resume()
    {
        Menu.SetActive(false);
    }

    #endregion
}
