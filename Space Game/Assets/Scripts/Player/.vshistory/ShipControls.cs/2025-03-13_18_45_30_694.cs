using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShipControls : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private UIManager _ui;
    [SerializeField] private QTControls _qtControls;
    [SerializeField] private GameObject playerShip;

    public static ShipControls instance;

    [Header("Movement")]
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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate
        }
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
        if (!_qtControls.loadingQT)
        {
            _ui.Resume();
            _input.SetShipControls();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
