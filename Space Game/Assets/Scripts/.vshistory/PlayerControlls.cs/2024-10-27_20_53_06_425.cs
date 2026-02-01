using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    [SerializeField] private InputReader _input;


    [SerializeField] private GameObject playerShip;

    public float maxSpeed;

    private Rigidbody rb;
    private float rollSpeed;
    private float speed;

    private void Start()
    {
        _input.MoveEvent += HandleMove;
        _input.RollEvent += HandleRoll;

        rb = playerShip.GetComponent<Rigidbody>();
    }

    private void HandleMove(Vector3 value)
    {
        // Calculate the direction relative to the ship's rotation
        Vector3 movement = transform.TransformDirection(value) * speed;
        if (movement.z == 1)
        {
            Debug.Log("Forward");
        }
        else if (movement.z == -1)
        {
            Debug.Log("Backwards");
        }

        // Apply this as the ship's velocity
        rb.velocity = movement;
    }


    private void HandleRoll(float value)
    {
        rollSpeed = value * -100;
    }

    private void Update()
    {
        playerShip.transform.Rotate(Vector3.forward, rollSpeed * Time.deltaTime);
    }
}
