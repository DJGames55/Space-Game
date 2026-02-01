using UnityEngine;

public class LightControls : MonoBehaviour
{
    public GameObject Player;

    private void Update()
    {
        if (Player != null)
        {
            transform.LookAt(Player.transform.position);
        }
    }
}
