using UnityEngine;

public class ShipControls : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private UIManager _ui;

    [SerializeField] private GameObject playerShip;

    public float maxSpeed;

    private Rigidbody rb;
    private float rollSpeed;
    private float speed;
    private Vector3 movementDirection;
    private Vector2 rotateSpeed;

    private void Start()
    {
        _input.MoveEvent += HandleMove;
        _input.RotateEvent += HandleRotate;
        _input.RollEvent += HandleRoll;

        _input.PauseEvent += Pause;
        _input.ResumeEvent += Resume;

        rb = playerShip.GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void HandleMove(Vector3 value)
    {
        movementDirection = value;
    }

    private void HandleRotate(Vector2 value)
    {
        rotateSpeed = new Vector2 (value.x * 50, value.y * -50);
    }

    private void HandleRoll(float value)
    {
        rollSpeed = value * -100;
    }

    private void Update()
    {
        // Roll rotation (Z-axis)
        playerShip.transform.Rotate(Vector3.forward, rollSpeed * Time.deltaTime);

        // Pitch and Yaw rotation (X and Y axes)
        playerShip.transform.Rotate(Vector3.up, rotateSpeed.x * Time.deltaTime, Space.World);  // Yaw (Y-axis)
        playerShip.transform.Rotate(Vector3.right, rotateSpeed.y * Time.deltaTime, Space.Self); // Pitch (X-axis)

        speed = maxSpeed;
        // Calculate the direction relative to the ship's rotation
        Vector3 movement = transform.TransformDirection(movementDirection) * speed;
        // Apply this as the ship's velocity
        rb.velocity = movement;
    }

    private void Pause()
    {
        
    }

    private void Resume()
    {

    }
}
