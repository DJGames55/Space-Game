using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    [SerializeField] private InputReader _input;

    private void Start()
    {
        _input.MoveEvent += HandleMove;
    }

    private void HandleMove(Vector3 value)
    {
        Debug.Log(value);
    }
}
