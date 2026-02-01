using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    [SerializeField] private InputReader _input;


    [SerializeField] private GameObject playerShip;

    public float speed;

    private Rigidbody rb;

    private void Start()
    {
        _input.MoveEvent += HandleMove;

        rb = playerShip.GetComponent<Rigidbody>();
    }

    private void HandleMove(Vector3 value)
    {
        rb.velocity = (value * speed);
    }  
}
