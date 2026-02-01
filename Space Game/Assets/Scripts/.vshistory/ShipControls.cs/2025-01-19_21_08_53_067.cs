using System.Collections;
using UnityEngine;

public class ShipControls : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private UIManager _ui;

    [SerializeField] private GameObject playerShip;
    [SerializeField] private Camera playerCamera;

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
        _input.UICloseEvent += Resume;

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
        rotateSpeed = (value * 50);
    }

    private void HandleRoll(float value)
    {
        rollSpeed = value * -100;
    }

    private void Update()
    {
        // Calculate roll rotation independently
        playerShip.transform.Rotate(Vector3.forward, rollSpeed * Time.deltaTime, Space.Self);

        // Store current rotation
        Quaternion currentRotation = playerShip.transform.rotation;

        // Calculate yaw (Y-axis rotation) and pitch (X-axis rotation) based on input
        Quaternion yaw = Quaternion.AngleAxis(rotateSpeed.x * Time.deltaTime, Vector3.up);   // Yaw rotation on world Y-axis
        Quaternion pitch = Quaternion.AngleAxis(-rotateSpeed.y * Time.deltaTime, Vector3.right); // Pitch rotation on local X-axis

        // Apply yaw and pitch to current rotation
        playerShip.transform.rotation = currentRotation * yaw * pitch;


        speed = maxSpeed;
        // Calculate the direction relative to the ship's rotation
        Vector3 movement = transform.TransformDirection(movementDirection) * speed;
        // Apply this as the ship's velocity
        rb.linearVelocity = movement;
    }


    private void Pause()
    {
        _ui.Pause();
        _input.SetUI();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        _ui.Resume();
        _input.SetShipControls();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    #region QT
    
    public void initiateQT(GameObject travelLocation)
    {
        StartCoroutine(QT(travelLocation));
    }

    public IEnumerator QT(GameObject travelLocation)
    {
        _ui.CloseQT();
        _input.DisableControls();

        // Define how long the rotation should take
        float rotationDuration = 1f; // 1 second to rotate
        float elapsedTime = 0f;

        // Cache the initial rotation and the target rotation
        Quaternion initialRotation = playerShip.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(travelLocation.transform.position - playerShip.transform.position);

        // Smoothly rotate the playerShip over time
        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;

            // Interpolate between the initial and target rotation
            playerShip.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / rotationDuration);

            yield return null; // Wait for the next frame
        }

        // Ensure final rotation is set (in case of minor discrepancies)
        playerShip.transform.rotation = targetRotation;

        bool atLocation = false;
        float QTSpeed = 0.5f;

        while (true) 
        {
            playerShip.transform.position = Vector3.Lerp(playerShip.transform.position, travelLocation.transform.position, Time.deltaTime * QTSpeed);
            break;
        }
        


        // Move the playerShip to the travel location
        playerShip.transform.position = travelLocation.transform.position;
        _input.SetShipControls();
    }

    #endregion
}
