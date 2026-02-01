using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Menu
    public GameObject Menu;
    #region Menu

    public void Pause()
    {
        Menu.SetActive(true);
    }

    public void Resume()
    {
        Menu.SetActive(false);
    }

    #endregion
}
