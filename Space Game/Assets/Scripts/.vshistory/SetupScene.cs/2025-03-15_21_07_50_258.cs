using UnityEngine;
using UnityEngine.SceneManagement;

public class SetupScene : MonoBehaviour
{
    [SerializeField] private UIManager _ui;
    [SerializeField] private QTControls _qtControls;
    [SerializeField] private SceneField startScene;

    private void Start()
    {
        _ui.currentScene = startScene;
        _ui.ReenableQTButtons();
        SceneManager.LoadScene(startScene);
    }
}
