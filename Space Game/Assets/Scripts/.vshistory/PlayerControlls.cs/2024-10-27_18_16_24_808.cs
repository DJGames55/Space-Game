using UnityEngine;
using UnityEngine.Animations;

public class PlayerControlls : MonoBehaviour
{
    [SerializeField] private InputReader _input;


    [SerializeField] private GameObject playerShip;

    public float speed;

    private Rigidbody rb;

    private void Start()
    {
        _input.MoveEvent += HandleMove;
        _input.RollEvent += HandleRoll;

        rb = playerShip.GetComponent<Rigidbody>();
    }

    private void HandleMove(Vector3 value)
    {
        rb.velocity = (value * speed);
    }  

    private void HandleRoll(float value)
    {
        playerShip.transform.Rotate(Vector3.forward, value * Time.deltaTime);
    }
}
